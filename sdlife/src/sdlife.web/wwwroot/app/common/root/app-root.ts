namespace sdlife {
    export var consts = {
        moduleName: "sdlife"
    };

    let app = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar", "ngMessages", "ngComponentRouter", "md.data.table"]);

    app.value("$routerRootComponent", "sdlifeAppRoot");    

    app.component("sdlifeAppRoot", {
        controller: () => { }, 
        controllerAs: "vm", 
        templateUrl: "/app/common/root/app-root.html", 
        $routeConfig: [
            { path: "/login", component: "loginPage", name: "Login" }, 
            { path: "/...", component: "sdlifeApp", name: "App" }, 
        ]
    });

    app.filter("nospace", () => {
        return (v: string) => {
            return v ? v.replace(/ /g, "") : "";
        };
    });

    app.filter("money", () => {
        return (v: number) => {
            return `¥${v.toFixed(2)}`;
        };
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

    export function isSmallDevice(media: ng.material.IMedia) {
        return media("xs");
    }

    export function ensure(
        dialog: ng.material.IDialogService,
        ev: Event,
        title: string,
        textContent: string = "") {
        return dialog.show(dialog.confirm()
            .title(title)
            .textContent(textContent)
            .ok("确定")
            .cancel("取消")
            .clickOutsideToClose(true)
            .targetEvent(<MouseEvent>ev));
    }

    app.config(["$httpProvider", ($httpProvider: ng.IHttpProvider) => {
        $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
        $httpProvider.defaults.headers.post["RequestVerificationToken"] = () => localStorage.getItem("csrf");
        $httpProvider.interceptors.push("authHttpInterceptor");
    }]);
}