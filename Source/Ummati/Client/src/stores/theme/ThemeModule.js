import Timer from "@/framework/Timer";
export const state = {
    hue: 211,
    theme: "light"
};
export const getters = {
// getFoo: s => (foo: number) => s.foo,
};
export const mutations = {
    hue: (s, p) => {
        s.hue = p;
        if (document) {
            document.documentElement.style.setProperty("--global-primary-colour-hue", s.hue.toString());
        }
    },
    theme: (s, p) => {
        s.theme = p;
        if (document) {
            const classList = document.documentElement.classList;
            if (s.theme === "light") {
                classList.remove("dark-theme");
                classList.remove("high-contrast-theme");
            }
            else if (s.theme === "dark") {
                classList.remove("high-contrast-theme");
                classList.add("dark-theme");
            }
            else {
                classList.remove("dark-theme");
                classList.add("high-contrast-theme");
            }
        }
    }
};
export const actions = {
    async transitionTheme() {
        if (document) {
            const classList = document.documentElement.classList;
            classList.add("theme-transition");
            await Timer.delay(750);
            classList.remove("theme-transition");
        }
    }
};
const mainModule = {
    actions,
    getters,
    mutations,
    namespaced: true,
    state
};
export default mainModule;
//# sourceMappingURL=ThemeModule.js.map