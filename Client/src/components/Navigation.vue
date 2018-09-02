<template>
  <nav>
    <router-link :to="{name: 'home'}"><Logo/></router-link>
    <router-link :to="{name: 'home'}">Home</router-link>
    <router-link :to="{name: 'about'}">About</router-link>
  </nav>
</template>

<script lang="ts">
import Vue from "vue";
import { Logo } from "@/components";

export default Vue.extend({
  name: "Navigation",
  components: {
    Logo
  }
});
</script>

<style lang="scss" scoped>
nav {
  background-color: lightblue;
  display: grid;
  grid-template-columns: auto auto auto 1fr;
  a {
    font-weight: bold;
    color: #2c3e50;
    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
