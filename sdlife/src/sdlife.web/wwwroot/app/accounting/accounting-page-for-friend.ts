/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingPageForFriend extends AccountingPage {
        dayClick(date: moment.Moment, ev: MouseEvent) {
        }

        eventClick(event: IAccountingEventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) {
        }

        eventDrop(event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) {
            rollback();
        }
    }

    module.component("accountingPageForFriend", {
        controller: AccountingPageForFriend,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page-for-friend.html",
    });
}