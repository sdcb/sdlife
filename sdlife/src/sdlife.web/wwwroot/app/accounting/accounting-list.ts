/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingList extends AccountingBasePage {
    }

    class AccountingListForFriend extends AccountingList {
    }

    module.component("accountingList", {
        controller: AccountingList,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-list.html",
        bindings: {
            "$router": "<"
        }
    });

    module.component("accountingListForFriend", {
        controller: AccountingListForFriend,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-list.html",
        bindings: {
            "$router": "<"
        }
    });
}