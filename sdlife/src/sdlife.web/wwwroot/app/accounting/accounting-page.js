/// <reference path="../../typings/tsd.d.ts" />
var sdlife;
(function (sdlife) {
    var accounting;
    (function (accounting) {
        var module = angular.module(accounting.consts.moduleName, ["ngMaterial", "ui.calendar"]);
        var AccountingPage = (function () {
            function AccountingPage($scope) {
                var _this = this;
                this.$scope = $scope;
                this.eventSource = [{
                        title: 'Long Event',
                        start: '2016-05-07',
                        end: '2016-05-10'
                    }];
                this.eventSources = [this.eventSource];
                this.calendarConfig = {
                    height: 450,
                    editable: true,
                    header: {
                        left: 'month basicWeek basicDay agendaWeek agendaDay',
                        center: 'title',
                        right: 'today prev,next'
                    },
                    dayClick: function () {
                        var args = [];
                        for (var _i = 0; _i < arguments.length; _i++) {
                            args[_i - 0] = arguments[_i];
                        }
                        return _this.dayClick.apply(_this, args);
                    },
                    eventDrop: this.$scope.alertOnDrop,
                    eventResize: this.$scope.alertOnResize
                };
                console.log(this);
            }
            AccountingPage.prototype.dayClick = function (day, ev) {
                this.eventSource.push({
                    title: 'Long Event',
                    start: day
                });
            };
            AccountingPage.$inject = ["$scope"];
            return AccountingPage;
        }());
        module.component("accountingPage", {
            controller: AccountingPage,
            controllerAs: "vm",
            templateUrl: "/app/accounting/accounting-page.html",
        });
    })(accounting = sdlife.accounting || (sdlife.accounting = {}));
})(sdlife || (sdlife = {}));
//# sourceMappingURL=accounting-page.js.map