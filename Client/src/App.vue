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
body {
  background: var(--global-background-colour);
  color: var(--global-text-colour);

  font-family: "Avenir", Helvetica, Arial, sans-serif;
}
.dark-theme {
  background: var(--global-background-colour);
  color: var(--global-text-colour);
}
.high-contrast-theme {
  background: var(--global-background-colour);
  color: var(--global-text-colour);
}
.theme-transition {
  transition: all 750ms !important;
  transition-delay: 0 !important;
}
</style>
