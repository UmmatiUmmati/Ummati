<template>
  <div :class="{'slideout-open': isOpen, 'slideout-opening': opening, 'slideout-closing': closing}">
    <div ref="menu" :class="'slideout-menu-' + side" class="slideout-menu">
      <slot name="menu"/>
    </div>
    <div :class="'slideout-panel-' + side" :style="{transform, 'transition-duration': duration + 'ms'}"
         class="slideout-panel"
         @touchstart="onTouchStart"
         @touchmove="onTouchMove"
         @touchcancel="onTouchCancel"
         @touchend="onTouchEnd">
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
    padding: {
      default: 256,
      type: Number
    },
    side: {
      default: "left",
      type: String
    },
    tolerance: {
      default: 70,
      type: Number
    },
    touch: {
      default: true,
      type: Boolean
    },
    value: {
      default: false,
      type: Boolean
    }
  },
  data() {
    return {
      startOffsetX: 0,
      currentOffsetX: 0,
      closing: false,
      opening: false,
      moved: false,
      isOpen: false,
      preventOpen: false,
      scrolling: false,
      scrollTimeout: undefined as NodeJS.Timer | undefined,
      transform: "",
      translateTo: 0,
      onScrollInternal: (e: Event) => {}
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
    this.translateTo = this.padding;
    this.translateTo *= this.orientation;

    if (this.touch && document) {
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

      await Timer.delay(this.duration + 50);

      this.transform = "";
      this.closing = false;
      this.$emit("input", this.isOpen);
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
      this.preventOpen =
        !this.touch || (!this.isOpen && this.menu.clientWidth !== 0);
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

      var dif_x = event.touches[0].clientX - this.startOffsetX;
      var translateX = (this.currentOffsetX = dif_x);

      if (Math.abs(translateX) > this.padding) {
        return;
      }

      if (Math.abs(dif_x) > 20) {
        this.opening = true;

        var oriented_dif_x = dif_x * this.orientation;

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
          translateX = dif_x + this.padding * this.orientation;
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
      var eve: Event;
      var tracking = false;

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

      node.addEventListener(event, captureEvent, false);

      return captureEvent;
    }
  }
});
</script>

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

.slideout-opening .slideout-menu,
.slideout-open .slideout-menu {
  display: block;
}
</style>
