/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingList extends AccountingBasePage {
        static $inject = ["accounting.api"];
        constructor(api: AccountingApi) {
            super(api);
            console.log(this);
        }
    }

    module.component("accountingList", {
        controller: AccountingList,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-list.html",
        bindings: {
            "$router": "<"
        }
    });
}