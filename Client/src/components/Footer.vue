<template>
  <footer v-once>
    © {{year}} Ummati
  </footer>
</template>

<script lang="ts">
import Vue from "vue";
import ClockService from "@/services/ClockService";

export default Vue.extend({
  name: "Footer",
  computed: {
    year() {
      return ClockService.now().getFullYear();
    }
  }
});
</script>
