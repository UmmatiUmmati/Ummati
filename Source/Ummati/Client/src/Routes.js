const routes = [
    {
        path: "/",
        name: "home",
        component: () => import(/* webpackChunkName: "home" */ "./views/Home.vue")
    },
    {
        path: "/about",
        name: "about",
        component: () => import(/* webpackChunkName: "about" */ "./views/About.vue")
    },
    {
        path: "/search",
        name: "search",
        component: () => import(/* webpackChunkName: "search" */ "./views/Search.vue")
    },
    {
        path: "/notifications",
        name: "notifications",
        component: () => import(/* webpackChunkName: "notifications" */ "./views/Notifications.vue")
    },
    {
        path: "/profile",
        name: "profile",
        component: () => import(/* webpackChunkName: "profile" */ "./views/Profile.vue")
    }
];
export default routes;
//# sourceMappingURL=Routes.js.map