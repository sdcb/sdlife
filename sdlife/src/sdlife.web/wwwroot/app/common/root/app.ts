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
            { path: "/book/me/calendar", component: "accountingCalendar", name: "BookCalendar" },
            { path: "/book/:userId/calendar", component: "accountingCalendarForFriend", name: "BookCalendarFriend" },
            { path: "/**", redirectTo: ["BookCalendar"] }
        ]
    });
}