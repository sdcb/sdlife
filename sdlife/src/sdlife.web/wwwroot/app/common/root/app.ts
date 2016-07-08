namespace sdlife {
    let app = angular.module(consts.moduleName);

    class AppController {
        $router: ng.Router;
    }

    app.component("sdlifeApp", {
        controller: AppController,
        controllerAs: "vm",
        templateUrl: "/app/common/root/app.html",
        bindings: {
            $router: "<",
        },
        $routeConfig: [
            { path: "/book/:userId/...", component: "accountingPage", name: "Book", useAsDefault: true },
            { path: "/", redirectTo: ["Book", { userId: "me" }] }
        ]
    });
}