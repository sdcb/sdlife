namespace sdlife {
    let app = angular.module(consts.moduleName);

    class AppController {
        $route: ng.Router;
    }

    app.component("sdlifeApp", {
        controller: AppController, 
        controllerAs: "vm", 
        templateUrl: "/app/common/root/app.html", 
        bindings: {
            $router: "<", 
        }, 
        $routeConfig: [
            { path: "/book", component: "accounting-page", name: "Book" },
            { path: "/book/:userId", component: "accounting-page-for-friend", name: "BookFriend" }, 
            { path: "/**", redirectTo: ["Book"] }
        ]
    });
}