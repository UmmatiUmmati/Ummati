<template>
  <dialog>
    <slot/>
  </dialog>
</template>

<script lang="ts">
// See https://github.com/GoogleChrome/dialog-polyfill
// See https://keithjgrant.com/posts/2018/meet-the-new-dialog-element/
import Vue from "vue";
import "dialog-polyfill/dialog-polyfill.js";

const dialogPolyfill = (window as any).dialogPolyfill;
delete (window as any).dialogPolyfill;

export default Vue.extend({
  props: {
    open: {
      default: false,
      type: Boolean
    }
  },
  watch: {
    open(value: boolean) {
      const dialog = this.$root;
      if (value) {
        (dialog as any).showModal();
      } else {
        (dialog as any).close();
      }
    }
  },
  created() {
    const dialog = this.$root;
    dialogPolyfill.registerDialog(dialog);
  }
});
</script>

<style lang="scss">
@import "~dialog-polyfill/dialog-polyfill";
</style>
