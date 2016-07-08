/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingPage extends AccountingBasePage {
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