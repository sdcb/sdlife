/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingPage {
        eventSource = <IAccountingEventObject[]>[];
        eventSources = [this.eventSource];
        calendarConfig = <IAccountingCalendarConfig>{
            height: 450,
            editable: true,
            header: {
                left: "month",
                center: "title",
                right: "today prev,next"
            },
            dayClick: (day, ev) => this.dayClick(day, ev),
            eventDrop: (event, duration, rollback) => this.eventDrop(event, duration, rollback),
            eventResize: (...args) => console.log(args), 
        }

        static $inject = ["api", "$mdDialog"];
        constructor(
            public api: AccountingApi,
            public dialog: ng.material.IDialogService
        ) {
            this.api.loadInMonth(moment()).then(dto => {
                let events = addColorToEventObjects(dto.map(x => mapEntityToCalendar(x)));
                this.eventSource.splice(0, this.eventSource.length, ...events);
            });
        }
        
        dayClick(date: moment.Moment, ev: MouseEvent) {
            showAccountingCreateDialog(date, this.dialog, ev).then((...args) => {
                console.log("then");
            }).catch((...args) => {
                console.log("catch");
            });
        }

        eventDrop(event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) {
        }
    }

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });
}