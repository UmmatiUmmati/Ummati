<template>
  <nav class="navigation">

    <div>L</div>
    <!-- <router-link :to="{name: 'home'}"><Logo/></router-link> -->

    <router-link :to="{name: 'home'}">
      <Icon size="2x" title="Home">
        <HomeIcon/>
      </Icon>
    </router-link>

    <router-link :to="{name: 'search'}">
      <Icon size="2x" title="Search">
        <SearchIcon/>
      </Icon>
    </router-link>

    <router-link :to="{name: 'notifications'}">
      <Icon size="2x" title="Notifications">
        <NotificationIcon/>
      </Icon>
    </router-link>

    <div @click="onProfileClick">Profile</div>

  </nav>
</template>

<script lang="ts">
import Vue from "vue";
import { createNamespacedHelpers } from "vuex";
import Icon from "@/components/icons/Icon.vue";
import HomeIcon from "@/components/icons/HomeIcon.vue";
import NotificationIcon from "@/components/icons/NotificationIcon.vue";
import SearchIcon from "@/components/icons/SearchIcon.vue";
import Logo from "@/components/Logo.vue";

const { mapMutations, mapState } = createNamespacedHelpers("main");

export default Vue.extend({
  name: "Navigation",
  components: {
    HomeIcon,
    Icon,
    Logo,
    NotificationIcon,
    SearchIcon
  },
  computed: mapState({}),
  methods: {
    ...mapMutations(["setIsProfileFlyoutOpen"]),
    onProfileClick() {
      this.setIsProfileFlyoutOpen(!this.isProfileFlyoutOpen);
    }
  }
});
</script>

<style lang="scss">
nav.navigation {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr 1fr;

  // a {
  //   &.router-link-exact-active {
  //   }
  // }
}
</style>
