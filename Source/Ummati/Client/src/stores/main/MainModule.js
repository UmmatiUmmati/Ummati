export const state = {
    isProfileFlyoutOpen: false
};
export const getters = {
// getFoo: s => (foo: number) => s.foo,
};
export const mutations = {
    isProfileFlyoutOpen: (s, p) => (s.isProfileFlyoutOpen = p)
};
export const actions = {
// async getFoo(context, bar: number) {}
};
const mainModule = {
    actions,
    getters,
    mutations,
    namespaced: true,
    state
};
export default mainModule;
//# sourceMappingURL=MainModule.js.map