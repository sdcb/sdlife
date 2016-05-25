﻿namespace sdlife {
    export var consts = {
        moduleName: "sdlife"
    };

    let app = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar", "ngMessages", "ngComponentRouter"]);

    app.value("$routerRootComponent", "sdlifeApp");    

    app.component("sdlifeApp", {
        controller: () => { }, 
        controllerAs: "vm", 
        templateUrl: "/app/common/app.html", 
        $routeConfig: [
            { path: "/login", component: "login-page", name: "Login" }, 
            { path: "/book", component: "accounting-page", name: "Book" }, 
            { path: "/**", redirectTo: ["Book"] }
        ]
    });

    app.factory("authHttpInterceptor", ["$q", ($q: ng.IQService) => {
        let interceptor = <ng.IHttpInterceptor>{
            responseError: (rejection) => {
                if (rejection.status === 401) {
                    location.href = "/#/login";
                }
                return $q.reject(rejection);
            }
        };
        return interceptor;
    }]);

    app.config(["$httpProvider", ($httpProvider: ng.IHttpProvider) => {
        $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
        let xsrfToken = $("[name=__RequestVerificationToken]").val();
        $httpProvider.defaults.headers.post["RequestVerificationToken"] = xsrfToken;
        $httpProvider.interceptors.push("authHttpInterceptor");
    }]);
}