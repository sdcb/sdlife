namespace sdlife {
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
            { path: "/book/:userId", component: "accounting-page-for-friend", name: "BookFriend" }, 
            { path: "/**", redirectTo: ["Book"] }
        ]
    });

    app.filter("nospace", () => {
        return (v: string) => {
            return v ? v.replace(/ /g, "") : "";
        };
    })

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
        $httpProvider.defaults.headers.post["RequestVerificationToken"] = () => localStorage.getItem("csrf");
        $httpProvider.interceptors.push("authHttpInterceptor");
    }]);
}