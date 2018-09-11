<template>
  <div :class="{ open: isSideVisible }" class="container">
    <transition name="flyout-right">
      <div v-show="isSideVisible" class="sidebar">
        <slot name="sidebar"/>
      </div>
    </transition>
    <div class="content" @click="setValue(false)">
      <slot/>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

type Position = "left" | "right";

export default Vue.extend({
  props: {
    position: {
      default: "left",
      type: String as () => Position
    },
    value: {
      default: false,
      type: Boolean
    }
  },
  data() {
    return {
      isSideVisible: false
    };
  },
  watch: {
    value() {
      this.setValue(this.value);
    }
  },
  methods: {
    setValue(value: boolean) {
      this.isSideVisible = value;
      this.$emit("input", this.isSideVisible);
    }
  }
});
</script>

<style lang="scss">
:root {
  --global-flyout-width: 20rem;
}
</style>


<style lang="scss" scoped>
.container {
  display: flex;
  overflow-x: hidden;
  position: relative;
}

.sidebar {
  background-color: var(--global-background-colour);
  height: 100%;
  position: absolute;
  width: var(--global-flyout-width);
}

.content {
  width: 100%;
}

.open {
  .content {
    opacity: 0.7;
    transform: translateX(var(--global-flyout-width));
  }
}

.flyout-left-enter-active,
.flyout-left-leave-active,
.flyout-right-enter-active,
.flyout-right-leave-active {
  transition-property: opacity transform;
  transition-duration: 0.3s;

  + .content {
    transition-property: opacity transform;
    transition-duration: 0.3s;
  }
}
.flyout-left-enter-active,
.flyout-right-enter-active {
  transition-timing-function: ease(out-quint);

  + .content {
    transition-timing-function: ease(out-quint);
  }
}
.flyout-left-leave-active,
.flyout-right-leave-active {
  transition-timing-function: ease(in-quint);

  + .content {
    transition-timing-function: ease(in-quint);
  }
}
.flyout-left-enter,
.flyout-right-leave-active {
  opacity: 0;
  transform: translate(-100%, 0);
}
.flyout-left-leave-active,
.flyout-right-enter {
  opacity: 0;
  transform: translate(-100%, 0);
}
</style>
