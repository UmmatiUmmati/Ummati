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
html {
  // Light Theme
  background: var(--global-light-background-colour);
  color: var(--global-light-text-colour);

  font-family: "Avenir", Helvetica, Arial, sans-serif;
}
.dark-theme {
  background: var(--global-dark-background-colour);
  color: var(--global-dark-text-colour);
}
.high-contrast-theme {
  background: var(--global-high-contrast-background-colour);
  color: var(--global-high-contrast-text-colour);
}
.theme-transition {
  transition: all 750ms !important;
  transition-delay: 0 !important;
}
</style>
