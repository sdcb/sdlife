import Vue = require("vue");
import VueRouter = require("vue-router");

const Foo = { template: '<div>foo</div>' }
const Bar = { template: '<div>bar</div>' }

const routes = [
    { path: '/foo', component: Foo },
    { path: '/bar', component: Bar }
]

const router = new VueRouter({
    routes: routes
})

const app = new Vue({
    router: router
}).$mount('#app')