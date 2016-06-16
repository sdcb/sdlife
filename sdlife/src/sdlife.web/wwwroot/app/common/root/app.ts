namespace sdlife {
    let app = angular.module(consts.moduleName);

    app.component("sdlifeApp", {
        controller: () => { }, 
        controllerAs: "vm", 
        templateUrl: "/app/common/root/app.html", 
        $routeConfig: [
            { path: "/book", component: "accounting-page", name: "Book" },
            { path: "/book/:userId", component: "accounting-page-for-friend", name: "BookFriend" }, 
            { path: "/**", redirectTo: ["Book"] }
        ]
    });
}