import { ActionTree, GetterTree, Module, MutationTree } from "vuex";
import Timer from "@/framework/Timer";
import IRootState from "@/stores/IRootState";
import IThemeState, { Theme } from "@/stores/theme/IThemeState";

export const state: IThemeState = {
  hue: 211,
  theme: "light"
};

export const getters: GetterTree<IThemeState, IRootState> = {
  // getFoo: s => (foo: number) => s.foo,
};

export const mutations: MutationTree<IThemeState> = {
  hue: (s, p: number) => {
    s.hue = p;
    if (document) {
      document.documentElement!.style.setProperty(
        "--global-primary-colour-hue",
        s.hue.toString()
      );
    }
  },
  theme: (s, p: Theme) => {
    s.theme = p;
    if (document) {
      const classList = document.documentElement!.classList;
      if (s.theme === "light") {
        classList.remove("dark-theme");
        classList.remove("high-contrast-theme");
      } else if (s.theme === "dark") {
        classList.remove("high-contrast-theme");
        classList.add("dark-theme");
      } else {
        classList.remove("dark-theme");
        classList.add("high-contrast-theme");
      }
    }
  }
};

export const actions: ActionTree<IThemeState, IRootState> = {
  async transitionTheme() {
    if (document) {
      const classList = document.documentElement!.classList;
      classList.add("theme-transition");
      await Timer.delay(750);
      classList.remove("theme-transition");
    }
  }
};

const mainModule: Module<IThemeState, IRootState> = {
  actions,
  getters,
  mutations,
  namespaced: true,
  state
};
export default mainModule;
