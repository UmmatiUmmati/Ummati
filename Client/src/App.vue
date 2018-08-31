<template>
  <div id="app">
    <Navigation/>
    <RouteTransition>
      <router-view/>
    </RouteTransition>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Navigation, RouteTransition } from "@/components";

export default Vue.extend({
  components: {
    Navigation,
    RouteTransition
  }
});
</script>

<style lang="scss">
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
