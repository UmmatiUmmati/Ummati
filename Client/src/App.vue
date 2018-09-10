<template>
  <div id="app">
    <header>
      <Navigation/>
    </header>
    <RouteTransition>
      <router-view/>
    </RouteTransition>
    <Footer/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Footer from "@/components/Footer.vue";
import Navigation from "@/components/Navigation.vue";
import RouteTransition from "@/components/RouteTransition.vue";

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
