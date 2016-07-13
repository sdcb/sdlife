/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingPage extends AccountingBasePage {
        currentNavItem: string;
        user() {
            return this.userId ? this.userId : "me";
        }

        getCurrentNavItemByRouter(router: ng.Router) {
            for (let r of this.routes) {
                let instrument = router.generate(r.state);                
                let isActive = router.isRouteActive(instrument);
                if (isActive) {
                    return r.name;
                }
            }
            throw new Error("non existing router");
        }

        routes = [
            { state: ["BookCalendar"], name: "calendar" },
            { state: ["BookList"], name: "list" }
        ];

        static $inject = ["accounting.api", "$timeout"];
        constructor(
            api: AccountingApi,
            public timeout: ng.ITimeoutService) {
            super(api);
        }

        $routerOnActivate(next) {
            super.$routerOnActivate(next);
            this.timeout(() => {
                this.currentNavItem = this.getCurrentNavItemByRouter(this.$router);
            }, 0);
        }
    }

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
        bindings: {
            "$router": "<"
        },
        $routeConfig: [
            { path: "/calendar", component: "accountingCalendar", name: "BookCalendar", useAsDefault: true },
            { path: "/list", component: "accountingList", name: "BookList" }
        ]
    });
}