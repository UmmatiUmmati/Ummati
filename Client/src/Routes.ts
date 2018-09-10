import { RouteConfig } from "vue-router";

const routes: RouteConfig[] = [
  {
    path: "/",
    name: "home",
    component: () => import(/* webpackChunkName: "home" */ "./views/Home.vue"),
    meta: {
      transition: "slide"
    }
  },
  {
    path: "/about",
    name: "about",
    component: () =>
      import(/* webpackChunkName: "about" */ "./views/About.vue"),
    meta: {
      transition: "slide"
    }
  },
  {
    path: "/search",
    name: "search",
    component: () =>
      import(/* webpackChunkName: "search" */ "./views/Search.vue"),
    meta: {
      transition: "slide"
    }
  },
  {
    path: "/profile",
    name: "profile",
    component: () =>
      import(/* webpackChunkName: "profile" */ "./views/Profile.vue"),
    meta: {
      transition: "slide"
    }
  }
];
export default routes;
