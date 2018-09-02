import Vue from "vue";
import Meta from "vue-meta";
import App from "./App.vue";
import router from "./Router";
import store from "@/stores/Store";
import "./RegisterServiceWorker";

Vue.config.productionTip = false;
Vue.use(Meta);

new Vue({
  render: x => x(App),
  router,
  store
}).$mount("#app");
