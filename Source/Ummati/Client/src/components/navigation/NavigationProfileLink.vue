<template>
  <div class="profile" @click.passive="onProfileClick">
    Profile
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import IMainState from "@/stores/main/IMainState.ts";

export default Vue.extend({
  name: "NavigationProfileLink",
  computed: {
    isProfileFlyoutOpen: {
      get(): boolean {
        return this.$store.state.main.isProfileFlyoutOpen;
      },
      set(value: boolean) {
        this.$store.commit("main/isProfileFlyoutOpen", value);
      }
    }
  },
  methods: {
    onProfileClick() {
      this.isProfileFlyoutOpen = !this.isProfileFlyoutOpen;
    }
  }
});
</script>

<style lang="scss" scoped>
.profile {
  --navigation-link-padding: 0.5rem;

  padding: var(--navigation-link-padding);
}
</style>
