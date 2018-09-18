<template>
  <div :class="{'slideout-opening': opening, 'slideout-closing': closing}">
    <div ref="menu" :class="'slideout-menu-' + side" class="slideout-menu">
      <slot name="menu"/>
    </div>
    <div :class="'slideout-panel-' + side" :style="{transform, 'transition-duration': duration + 'ms'}"
         class="slideout-panel"
         @click.passive="onClick"
         @touchstart.passive="onTouchStart"
         @touchmove.passive="onTouchMove"
         @touchcancel.passive="onTouchCancel"
         @touchend.passive="onTouchEnd">
      <slot/>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Timer from "@/framework/Timer";

export default Vue.extend({
  props: {
    duration: {
      default: 300,
      type: Number
    },
    side: {
      default: "left",
      type: String as () => "left" | "right"
    },
    tolerance: {
      default: 70,
      type: Number
    },
    value: {
      default: false,
      type: Boolean
    },
    width: {
      default: 256,
      type: Number
    }
  },
  data() {
    return {
      closing: false,
      opening: false,
      moved: false,
      isOpen: false,
      preventOpen: false,
      scrolling: false,
      scrollTimeout: undefined as NodeJS.Timer | undefined,
      onScrollInternal: (e: Event) => {},
      startOffsetX: 0,
      currentOffsetX: 0,
      transform: "",
      translateTo: 0
    };
  },
  computed: {
    orientation(): number {
      return this.side === "right" ? -1 : 1;
    },
    menu(): HTMLElement {
      return this.$refs.menu as HTMLElement;
    }
  },
  watch: {
    value() {
      if (this.isOpen) {
        this.close();
      } else {
        this.open();
      }
    }
  },
  created() {
    this.translateTo = this.width;
    this.translateTo *= this.orientation;

    if (document) {
      const self = this;
      this.onScrollInternal = this.decouple(document, "scroll", function() {
        // Decouple scroll event
        if (!self.moved) {
          if (self.scrollTimeout) {
            clearTimeout(self.scrollTimeout);
          }
          self.scrolling = true;
          self.scrollTimeout = setTimeout(function() {
            self.scrolling = false;
          }, 250);
        }
      });
    }
  },
  destroyed() {
    if (document) {
      document.removeEventListener("scroll", this.onScrollFn);
    }
  },
  methods: {
    async open() {
      this.$emit("opening");
      this.translateXTo(this.translateTo);
      this.opening = true;
      this.isOpen = true;
      if (document) {
        document.documentElement.classList.add("slideout-open");
      }

      await Timer.delay(this.duration + 50);

      this.opening = false;
      this.$emit("input", this.isOpen);
    },
    async close() {
      if (!this.isOpen && !this.opening) {
        return;
      }

      this.$emit("closing");
      this.translateXTo(0);
      this.closing = true;
      this.isOpen = false;
      if (document) {
        document.documentElement.classList.remove("slideout-open");
      }

      await Timer.delay(this.duration + 50);

      this.transform = "";
      this.closing = false;
      this.$emit("input", this.isOpen);
    },
    onClick() {
      if (this.isOpen) {
        this.close();
      }
    },
    onTouchStart(event: TouchEvent) {
      // Resets values on touchstart
      if (event.touches === undefined) {
        return;
      }
      this.moved = false;
      this.closing = false;
      this.opening = false;
      this.startOffsetX = event.touches[0].pageX;
      this.preventOpen = !this.isOpen && this.menu.clientWidth !== 0;
    },
    onTouchCancel() {
      // Resets values on touchcancel
      this.moved = false;
      this.closing = false;
      this.opening = false;
    },
    onTouchEnd() {
      // Toggles slideout on touchend
      if (this.moved) {
        this.$emit("translateend");
        this.opening && Math.abs(this.currentOffsetX) > this.tolerance
          ? this.open()
          : this.close();
      }
      this.moved = false;
      this.closing = false;
      this.opening = false;
    },
    onTouchMove(event: TouchEvent) {
      // Translates panel on touchmove
      if (
        this.scrolling ||
        this.preventOpen ||
        event.touches === undefined ||
        this.hasIgnoredElements(event.target as HTMLElement)
      ) {
        return;
      }

      const dif_x = event.touches[0].clientX - this.startOffsetX;
      let translateX = (this.currentOffsetX = dif_x);

      if (Math.abs(translateX) > this.width) {
        return;
      }

      if (Math.abs(dif_x) > 20) {
        this.opening = true;

        const oriented_dif_x = dif_x * this.orientation;

        if (
          (this.isOpen && oriented_dif_x > 0) ||
          (!this.isOpen && oriented_dif_x < 0)
        ) {
          return;
        }

        if (!this.moved) {
          this.$emit("translatestart");
        }

        if (oriented_dif_x <= 0) {
          translateX = dif_x + this.width * this.orientation;
          this.opening = false;
        }

        this.transform = "translateX(" + translateX + "px)";
        this.$emit("translate", translateX);
        this.moved = true;
      } else if (this.isOpen) {
        this.closing = true;
      }
    },
    onScrollFn(e: Event) {
      return this.onScrollInternal(e);
    },
    translateXTo(translateX: number) {
      // Translates panel and updates currentOffset with a given X point
      this.currentOffsetX = translateX;
      this.transform = "translateX(" + translateX + "px)";
    },
    hasIgnoredElements(element: HTMLElement | undefined) {
      if (element) {
        while (element.parentNode) {
          if (element.getAttribute("data-slideout-ignore") !== null) {
            return element;
          }
          element = element.parentNode as HTMLElement;
        }
      }
      return null;
    },
    decouple(node: Node, event: string, fn: () => void) {
      let eve: Event;
      let tracking = false;

      function captureEvent(e: Event) {
        eve = e;
        track();
      }

      function track() {
        if (!tracking) {
          window.requestAnimationFrame(update);
          tracking = true;
        }
      }

      function update() {
        fn.call(node, eve);
        tracking = false;
      }

      node.addEventListener(event, captureEvent, { passive: true });

      return captureEvent;
    }
  }
});
</script>

<style lang="scss">
.slideout-open {
  body {
    overflow: hidden;
  }
}
</style>

<style lang="scss" scoped>
.slideout-menu {
  display: none;
  bottom: 0;
  min-height: 100vh;
  // overflow-y: scroll;
  // -webkit-overflow-scrolling: touch;
  position: fixed;
  top: 0;
  width: 256px;
  z-index: 0;
}
.slideout-menu-left {
  left: 0;
}
.slideout-menu-right {
  right: 0;
}
.slideout-opening .slideout-menu,
.slideout-open .slideout-menu {
  display: block;
}

.slideout-panel {
  background-color: #fff; /* A background-color is required */
  min-height: 100vh;
  position: relative;
  will-change: transform;
  z-index: 1;
}
.slideout-open .slideout-panel {
  overflow: hidden;
}
.slideout-opening .slideout-panel {
  transition-property: transform;
  transition-timing-function: ease(out-quint);
}
.slideout-closing .slideout-panel {
  transition-property: transform;
  transition-timing-function: ease(in-quint);
}

// .slideout-panel:before {
//   content: "";
//   display: block;
//   background-color: rgba(0, 0, 0, 0);
// }
// .slideout-opening .slideout-panel:before {
//   transition: background-color 0.5s ease(out-quint);
//   background-color: rgba(0, 0, 0, 0.5);
// }
// .slideout-closing .slideout-panel:before {
//   transition: background-color 0.5s ease(in-quint);
//   background-color: rgba(0, 0, 0, 0);
// }
// .slideout-openening .slideout-panel:before,
// .slideout-closing .slideout-panel:before,
// .slideout-open .slideout-panel:before {
//   position: absolute;
//   top: 0;
//   bottom: 0;
//   width: 100%;
//   background-color: rgba(0, 0, 0, 0.5);
//   z-index: 99;
// }
</style>
