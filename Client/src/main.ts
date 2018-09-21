import Vue from "vue";
import Meta from "vue-meta";
// import Vuelidate from "vuelidate";
import App from "./App.vue";
import router from "./Router";
import store from "@/stores/Store";
import "./RegisterServiceWorker";

// Use Idle Until Urgent pattern. See https://github.com/GoogleChromeLabs/idlize.

Vue.config.productionTip = false;

Vue.use(Meta);
// Vue.use(Vuelidate);

new Vue({
  render: x => x(App),
  router,
  store
}).$mount("#app");
