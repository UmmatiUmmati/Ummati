<template>
  <div>
    <input id="light-theme-radio" v-model="themeInternal" type="radio" value="light">
    <label for="light-theme-radio">Light</label>

    <input id="dark-theme-radio" v-model="themeInternal" type="radio" value="dark">
    <label for="dark-theme-radio">Dark</label>

    <input id="high-contrast-theme-radio" v-model="themeInternal" type="radio" value="high-contrast">
    <label for="high-contrast-theme-radio">High Contrast</label>

    <label for="hue-range">Colour</label>
    <input id="hue-range" v-model="hueInternal" type="range" min="0" max="360">
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { createNamespacedHelpers } from "vuex";
import IRootState from "@/stores/IRootState";
import { Theme } from "@/stores/theme/IThemeState";

const { mapActions, mapMutations } = createNamespacedHelpers("theme");

export default Vue.extend({
  metaInfo: {
    title: "Profile"
  },
  computed: {
    hueInternal: {
      get(): number {
        return (this.$store.state as IRootState).theme.hue;
      },
      set(value: number) {
        this.hue(value);
        this.transitionTheme();
      }
    },
    themeInternal: {
      get(): Theme {
        return (this.$store.state as IRootState).theme.theme;
      },
      set(value: Theme) {
        this.theme(value);
        this.transitionTheme();
      }
    }
  },
  methods: {
    ...mapMutations(["hue", "theme"]),
    ...mapActions(["transitionTheme"])
  }
});
</script>
