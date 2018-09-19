<template>
  <router-link :to="to" class="link">
    <Icon :title="title" :value="icon" class="icon" size="2x"/> <span class="title">{{title}}</span>
  </router-link>
</template>

<script lang="ts">
import Vue from "vue";
import Icon from "@/components/Icon.vue";

export default Vue.extend({
  name: "NavigationLink",
  components: {
    Icon
  },
  props: {
    icon: {
      required: true,
      type: String
    },
    title: {
      required: true,
      type: String
    },
    to: {
      required: true,
      type: Object
    }
  }
});
</script>

<style lang="scss" scoped>
.link {
  --navigation-link-border-width: 0.125rem;
  --navigation-link-colour: var(--global-primary-colour);
  --navigation-link-icon-stroke-width: 0.1rem;
  --navigation-link-grid-gap: 0.5rem;
  --navigation-link-padding: 0.5rem;

  align-items: center;
  color: var(--navigation-link-colour);
  display: grid;
  grid-gap: var(--navigation-link-grid-gap);
  grid-template-columns: auto auto;
  padding: var(--navigation-link-padding);

  &:hover {
    text-decoration: none;
  }

  &.router-link-exact-active {
    border-bottom-color: var(--navigation-link-colour);
    border-bottom-style: solid;
    border-bottom-width: var(--navigation-link-border-width);
  }

  .icon {
    stroke: var(--navigation-link-colour);
    stroke-width: var(--navigation-link-icon-stroke-width);
  }
}
</style>
