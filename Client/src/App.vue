<template>
  <div id="app">
    <Navigation/>
    <main>
      <RouteTransition>
        <router-view/>
      </RouteTransition>
    </main>
    <Footer/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Footer, Navigation, RouteTransition } from "@/components";

export default Vue.extend({
  components: {
    Footer,
    Navigation,
    RouteTransition
  },
  metaInfo: {
    titleTemplate: "%s - Ummati"
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
