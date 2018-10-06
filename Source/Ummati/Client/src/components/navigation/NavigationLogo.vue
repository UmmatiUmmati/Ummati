<template>
  <router-link :to="{name: 'home'}" class="logo">
    <Icon size="2x" title="Ummati" value="logo"/>
  </router-link>
</template>

<script lang="ts">
import Vue from "vue";
import Icon from "@/components/Icon.vue";

export default Vue.extend({
  name: "NavigationLogo",
  components: {
    Icon
  }
});
</script>

<style lang="scss" scoped>
.logo {
  --navigation-logo-colour: var(--global-primary-colour);
  --navigation-logo-padding: 0.5rem;

  fill: var(--navigation-logo-colour);
  padding: var(--navigation-logo-padding);
}
</style>
