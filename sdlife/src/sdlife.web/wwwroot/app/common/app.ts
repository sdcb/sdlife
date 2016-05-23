namespace sdlife {
    export var consts = {
        moduleName: "sdlife",
        version: new Date().getTime(),
    };

    let module = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar", "ngMessages", "ngComponentRouter"]);

    module.value("$routerRootComponent", "sdlifeApp");

    module.component("sdlifeApp", {
        controller: () => { }, 
        controllerAs: "vm", 
        templateUrl: "/app/common/app.html", 
        $routeConfig: [
            { path: "/login", component: "login-page", name: "Login" }, 
            { path: "/book", component: "accounting-page", name: "Book" }, 
            { path: "/**", redirectTo: ["Book"] }
        ]
    });
}