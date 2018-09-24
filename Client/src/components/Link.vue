<template>
  <router-link :to="to" class="link">
    <slot/>
  </router-link>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "Link",
  props: {
    to: {
      required: true,
      type: Object
    }
  }
});
</script>

<style lang="scss" scoped>
.link {
  --link-border-colour: var(--global-primary-colour);
  --link-border-width: 0.125rem;
  --link-colour: var(--global-text-colour);
  // --link-icon-stroke-width: 0.1rem;
  // --link-grid-gap: 0.5rem;
  // --link-padding: 0.5rem;

  background: linear-gradient(
    to bottom,
    var(--link-border-colour) 0%,
    var(--link-border-colour) 100%
  );
  background-position: 0 100%;
  background-repeat: repeat-x;
  background-size: 4px 4px;
  color: var(--link-colour);
  text-decoration: none;
  transition: background-size 0.2s;

  // align-items: center;
  // color: var(--link-colour);
  // display: grid;
  // grid-gap: var(--navigation-link-grid-gap);
  // grid-template-columns: auto auto;
  // padding: var(--navigation-link-padding);

  &:hover {
    background-size: 4px 50px;
  }

  // &.router-link-exact-active {
  //   border-bottom-color: var(--navigation-link-colour);
  //   border-bottom-style: solid;
  //   border-bottom-width: var(--navigation-link-border-width);
  // }
}
</style>
