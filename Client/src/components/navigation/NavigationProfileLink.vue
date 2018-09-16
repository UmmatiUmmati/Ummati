<template>
  <div class="profile" @click="onProfileClick">
    Profile
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { createNamespacedHelpers } from "vuex";
import IMainState from "@/stores/main/IMainState.ts";

const { mapMutations, mapState } = createNamespacedHelpers("main");

export default Vue.extend({
  name: "NavigationProfileLink",
  computed: {
    ...mapState({
      isProfileFlyoutOpen: (state: IMainState) => state.isProfileFlyoutOpen
    })
  },
  methods: {
    ...mapMutations(["setIsProfileFlyoutOpen"]),
    onProfileClick() {
      this.setIsProfileFlyoutOpen(!this.isProfileFlyoutOpen);
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
