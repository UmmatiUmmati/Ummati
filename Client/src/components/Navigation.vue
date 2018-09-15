<template>
  <nav class="navigation">

    <router-link :to="{name: 'home'}" class="navigation-logo">
      <Logo/>
    </router-link>

    <router-link :to="{name: 'home'}" class="navigation-link">
      <Icon size="2x" title="Home">
        <HomeIcon/>
      </Icon> Home
    </router-link>

    <router-link :to="{name: 'notifications'}" class="navigation-link">
      <Icon size="2x" title="Notifications">
        <NotificationIcon/>
      </Icon> Notifications
    </router-link>

    <router-link :to="{name: 'search'}" class="navigation-link">
      <Icon size="2x" title="Search">
        <SearchIcon/>
      </Icon> Search
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
.navigation {
  --navigation-colour: var(--global-primary-colour);

  display: grid;
  color: var(--navigation-colour);
  grid-template-columns: auto 1fr 1fr 1fr 1fr;

  // a {
  //   &.router-link-exact-active {
  //   }
  // }
}
.navigation-logo {
  height: 4rem;
  padding: 1rem;
  width: 4rem;
}
.navigation-link {
  color: var(--navigation-colour);
  display: grid;
  fill: var(--navigation-colour);
  grid-gap: 0.5rem;
  grid-template-columns: auto auto;
  padding: 1rem;

  &.router-link-exact-active {
    border-bottom: 1px solid var(--navigation-colour);
  }

  &:hover {
    text-decoration: none;
  }
}
</style>
