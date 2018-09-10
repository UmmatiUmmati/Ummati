<template>
  <div>
    <slot v-if="isOnline" name="online" />
    <slot v-else name="offline" />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

const networkEvents = ["online", "offline"];
function getOnlineStatus() {
  if (navigator) {
    return navigator.onLine || false;
  }
  return true;
}

export default Vue.extend({
  name: "OnlineStatus",
  data() {
    return {
      isOnline: getOnlineStatus()
    };
  },
  created() {
    networkEvents.forEach(event =>
      window.addEventListener(event, this.updateStatus)
    );
  },
  beforeDestroy() {
    networkEvents.forEach(event =>
      window.removeEventListener(event, this.updateStatus)
    );
  },
  methods: {
    updateStatus() {
      this.isOnline = getOnlineStatus();
      this.$emit("change", this.isOnline);
    }
  }
});
</script>
