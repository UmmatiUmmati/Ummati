import Vue from "vue";
import App from "./App.vue";
import router from "./Router";
import store from "./store";
import "./RegisterServiceWorker";

Vue.config.productionTip = false;

new Vue({
  render: x => x(App),
  router,
  store
}).$mount("#app");
