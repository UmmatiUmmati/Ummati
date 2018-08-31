import Vue from "vue";
import Vuex, { ModuleTree, Store } from "vuex";
import IRootState from "@/stores/IRootState";
import mainModule from "@/stores/main/MainModule";

Vue.use(Vuex);

const modules: ModuleTree<IRootState> = {
  main: mainModule
};

const store = new Store({
  modules,
  // Enable strict mode when running in development. This stops you from directly changing state without a mutation.
  strict: process.env.NODE_ENV === "development"
});
export default store;

// Enable hot module reloading for store modules.
if ((module as any).hot) {
  (module as any).hot.accept(["./main/MainModule"], () => {
    store.hotUpdate({
      modules: {
        main: require("./main/MainModule").default
      }
    });
  });
}
