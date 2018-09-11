import { ActionTree, GetterTree, Module, MutationTree } from "vuex";
import IRootState from "@/stores/IRootState";
import IMainState from "@/stores/main/IMainState";

export const state: IMainState = {
  isProfileFlyoutOpen: false
};

export const getters: GetterTree<IMainState, IRootState> = {
  // getFoo: s => (foo: number) => s.foo,
};

export const mutations: MutationTree<IMainState> = {
  setIsProfileFlyoutOpen: (s, p: boolean) => (s.isProfileFlyoutOpen = p)
};

export const actions: ActionTree<IMainState, IRootState> = {
  // async getFoo(context, bar: number) {}
};

const mainModule: Module<IMainState, IRootState> = {
  actions,
  getters,
  mutations,
  namespaced: true,
  state
};
export default mainModule;
