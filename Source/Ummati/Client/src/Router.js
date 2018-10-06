import Vue from "vue";
import Router from "vue-router";
import routes from "@/Routes";
Vue.use(Router);
export default new Router({
    mode: "history",
    base: process.env.BASE_URL,
    routes,
    scrollBehavior: (to, from, savedPosition) => {
        if (savedPosition) {
            return savedPosition;
        }
        else if (to.hash) {
            return {
                selector: to.hash
            };
        }
        else {
            return { x: 0, y: 0 };
        }
    }
});
//# sourceMappingURL=Router.js.map