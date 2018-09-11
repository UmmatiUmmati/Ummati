<template>
  <div id="app">
    <Flyout :value="isProfileFlyoutOpen">
      <template slot="sidebar">
        <aside>
          <h1>Profile</h1>
          This is to show profile information.
        </aside>
      </template>
      <div>
        <header>
          <Navigation/>
        </header>
        <RouteTransition>
          <router-view/>
        </RouteTransition>
        <Footer/>
      </div>
    </Flyout>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { createNamespacedHelpers } from "vuex";
import Flyout from "@/components/Flyout.vue";
import Footer from "@/components/Footer.vue";
import Navigation from "@/components/Navigation.vue";
import RouteTransition from "@/components/RouteTransition.vue";

const { mapState } = createNamespacedHelpers("main");

export default Vue.extend({
  components: {
    Flyout,
    Footer,
    Navigation,
    RouteTransition
  },
  metaInfo: {
    titleTemplate: "%s - Ummati"
  },
  computed: mapState({})
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
