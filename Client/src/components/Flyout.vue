<template>
  <div :class="{'flyout-opening': opening, 'flyout-closing': closing, 'flyout-left': isLeftSide, 'flyout-right': isRightSide}" class="flyout">
    <div ref="sidebar" :style="{width: sidebarWidth}" class="flyout-sidebar">
      <slot name="sidebar"/>
    </div>
    <div :style="{transform: contentTransform, 'transition-duration': contentTransitionDuration}"
         class="flyout-content"
         @click.passive="onContentClick"
         @touchstart.passive="onContentTouchStart"
         @touchmove.passive="onContentTouchMove"
         @touchcancel.passive="onContentTouchCancel"
         @touchend.passive="onContentTouchEnd">
      <slot/>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Timer from "@/framework/Timer";

type Side = "left" | "right";

export default Vue.extend({
  props: {
    duration: {
      default: 300,
      type: Number
    },
    side: {
      default: "left",
      type: String as () => Side
    },
    // Only starts moving the sidebar when the user drags for more than the specified threshold.
    threshold: {
      default: 20,
      type: Number
    },
    // The number of px needed for the sidebar can be opened completely, otherwise it closes.
    tolerance: {
      default: 70,
      type: Number
    },
    // True if the flyout is open, otherwise false.
    value: {
      default: false,
      type: Boolean
    },
    // The width of the flyout.
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
      translateX: 0,
      translateTo: 0
    };
  },
  computed: {
    contentTransform(): string {
      return `translateX(${this.translateX}px)`;
    },
    contentTransitionDuration(): string {
      return `${this.duration}ms`;
    },
    isLeftSide(): boolean {
      return this.side === "left";
    },
    isRightSide(): boolean {
      return this.side === "right";
    },
    sidebarWidth(): string {
      return `${this.width}px`;
    },
    orientation(): number {
      return this.side === "right" ? -1 : 1;
    },
    sidebarClientWidth(): number {
      const sidebar = this.$refs.sidebar as Element;
      return sidebar.clientWidth;
    }
  },
  watch: {
    value(newValue, oldValue) {
      if (this.value) {
        this.open();
      } else {
        this.close();
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
        document.documentElement.classList.add("flyout-open");
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
        document.documentElement.classList.remove("flyout-open");
      }

      await Timer.delay(this.duration + 50);

      this.translateX = 0;
      this.closing = false;
      this.$emit("input", this.isOpen);
    },
    onContentClick() {
      if (this.isOpen) {
        this.close();
      }
    },
    onContentTouchStart(event: TouchEvent) {
      if (event.touches === undefined) {
        return;
      }
      // Reset values
      this.moved = false;
      this.closing = false;
      this.opening = false;
      this.startOffsetX = event.touches[0].pageX;
      this.preventOpen = !this.isOpen && this.sidebarClientWidth !== 0;
    },
    onContentTouchCancel() {
      // Reset values
      this.moved = false;
      this.closing = false;
      this.opening = false;
    },
    onContentTouchMove(event: TouchEvent) {
      // Translates content
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

      if (Math.abs(dif_x) > this.threshold) {
        this.opening = true;

        const oriented_dif_x = dif_x * this.orientation;

        if (
          (this.isOpen && oriented_dif_x > 0) ||
          (!this.isOpen && oriented_dif_x < 0)
        ) {
          return;
        }

        // if (!this.moved) {
        //   this.$emit("translatestart");
        // }

        if (oriented_dif_x <= 0) {
          translateX = dif_x + this.width * this.orientation;
          this.opening = false;
        }

        this.translateX = translateX;
        // this.$emit("translate", this.translateX);
        this.moved = true;
      } else if (this.isOpen) {
        this.closing = true;
      }
    },
    onContentTouchEnd() {
      // Toggles flyout
      if (this.moved) {
        // this.$emit("translateend");
        this.opening && Math.abs(this.currentOffsetX) > this.tolerance
          ? this.open()
          : this.close();
      }
      this.moved = false;
      this.closing = false;
      this.opening = false;
    },
    onScrollFn(event: Event) {
      return this.onScrollInternal(event);
    },
    translateXTo(translateX: number) {
      // Translates content and updates currentOffset with a given X point
      this.currentOffsetX = translateX;
      this.translateX = translateX;
    },
    hasIgnoredElements(element: HTMLElement | undefined) {
      if (element) {
        while (element.parentNode) {
          if (element.getAttribute("data-flyout-ignore") !== null) {
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
.flyout {
  --flyout-content-background-colour: var(--global-background-colour);
  --flyout-sidebar-background-colour: var(--global-background-colour);
}

.flyout-content {
  // A background-color is required
  background-color: var(--flyout-content-background-colour);
  min-height: 100vh;
  position: relative;
  will-change: transform;
  z-index: 1;
}

.flyout-sidebar {
  background-color: var(--flyout-sidebar-background-colour);
  display: none;
  bottom: 0;
  min-height: 100vh;
  overflow-y: auto;
  position: fixed;
  top: 0;
  z-index: 0;
}

.flyout-left {
  .flyout-sidebar {
    left: 0;
  }
}

.flyout-right {
  .flyout-sidebar {
    right: 0;
  }
}

.flyout-opening {
  .flyout-sidebar {
    display: block;
  }

  .flyout-content {
    transition-property: transform;
    transition-timing-function: ease(out-quint);
  }
}

.flyout-closing {
  .flyout-sidebar {
    display: block;
  }

  .flyout-content {
    transition-property: transform;
    transition-timing-function: ease(in-quint);
  }
}

.flyout-open {
  body {
    overflow: hidden;
  }

  .flyout-content {
    overflow: hidden;
  }

  .flyout-sidebar {
    display: block;
  }
}

// .flyout-content:before {
// content: "";
// display: block;
// // background-color: rgba(0, 0, 0, 0);
// }
// .flyout-opening {
// .flyout-content:before {
// // background-color: rgba(0, 0, 0, 0.5);
// transition: background-color 0.5s ease(out-quint);
// }
// }
// .flyout-closing {
// .flyout-content:before {
// // background-color: rgba(0, 0, 0, 0);
// transition: background-color 0.5s ease(in-quint);
// }
// }
// .flyout-open .flyout-content:before {
// position: absolute;
// top: 0;
// bottom: 0;
// width: 100%;
// background-color: rgba(0, 0, 0, 0.5);
// z-index: 99;
// }
</style>
