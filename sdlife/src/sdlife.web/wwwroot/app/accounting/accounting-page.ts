/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar"]);

    class AccountingPage {
        eventSource = <any[]>[{
            title: 'Long Event',
            start: '2016-05-07',
            end: '2016-05-10'
        }];
        eventSources = [this.eventSource];
        calendarConfig = {
            height: 450,
            editable: true,
            header: {
                left: 'month basicWeek basicDay agendaWeek agendaDay',
                center: 'title',
                right: 'today prev,next'
            },
            dayClick: (...args) => this.dayClick(...args),
            eventDrop: this.$scope.alertOnDrop,
            eventResize: this.$scope.alertOnResize
        }

        static $inject = ["$scope"];
        constructor(public $scope: any) {
            console.log(this);
        }

        dayClick(...args)
        dayClick(day: moment.Moment, ev: Event) {
            this.eventSource.push({
                title: 'Long Event',
                start: day
            });
        }
    }

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });
}