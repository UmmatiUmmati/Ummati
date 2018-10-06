<template>
  <transition
    :name="transition"
    mode="out-in">
    <slot/>
  </transition>
</template>

<script lang="ts">
import Vue from "vue";
import { Route } from "vue-router";

export default Vue.extend({
  name: "RouteTransition",
  data() {
    return {
      transition: "fade"
    };
  },
  watch: {
    $route(to: Route, from: Route) {
      const toTransition = to.meta.transition;
      if (toTransition === "slide") {
        const toDepth = this.getDepth(to.path);
        const fromDepth = this.getDepth(from.path);
        this.transition = toDepth < fromDepth ? "slide-right" : "slide-left";
      } else {
        this.transition = toTransition === undefined ? "fade" : toTransition;
      }
    }
  },
  methods: {
    getDepth(path: string) {
      if (path === "/") {
        return 1;
      }
      return path.split("/").length;
    }
  }
});
</script>
