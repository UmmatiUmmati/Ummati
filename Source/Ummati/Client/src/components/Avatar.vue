<template>
  <div class="avatar">
    <img :alt="alt" :src="source" class="avatar-image"/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "Avatar",
  props: {
    alt: {
      required: true,
      type: String
    },
    source: {
      required: true,
      type: String
    }
  }
});
</script>

<style lang="scss">
.avatar {
  --avatar-border-radius: 50%;
  --avatar-fallback-colour: #cccccc;
  --avatar-size: 5em;

  position: relative;
  width: var(--avatar-size);
  height: var(--avatar-size);
  border-radius: var(--avatar-border-radius);
  overflow: hidden;
  background: var(--avatar-fallback-colour);
}

.avatar-image {
  position: static;
  object-fit: cover;
  width: 100%;
  height: 100%;
}
</style>
