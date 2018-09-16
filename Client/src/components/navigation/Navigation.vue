<template>
  <nav class="navigation">

    <NavigationLogo/>

    <NavigationLink :to="{name: 'home'}" icon="home" title="Home"/>
    <NavigationLink :to="{name: 'notifications'}" icon="notification" title="Notifications"/>
    <NavigationLink :to="{name: 'search'}" icon="search" title="Search"/>

    <NavigationProfileLink class="profile"/>

  </nav>
</template>

<script lang="ts">
import Vue from "vue";
import NavigationLink from "@/components/navigation/NavigationLink.vue";
import NavigationLogo from "@/components/navigation/NavigationLogo.vue";
import NavigationProfileLink from "@/components/navigation/NavigationProfileLink.vue";

export default Vue.extend({
  name: "Navigation",
  components: {
    NavigationLink,
    NavigationLogo,
    NavigationProfileLink
  }
});
</script>

<style lang="scss" scoped>
.navigation {
  align-items: center;
  display: grid;
  grid-template-columns: auto auto auto auto 1fr auto;
}

.profile {
  grid-column: 6 / 7;
}
</style>
