/// <reference path="../../typings/tsd.d.ts" />
var sdlife;
(function (sdlife) {
    var accounting;
    (function (accounting) {
        var module = angular.module(accounting.consts.moduleName);
        var AccountingPage = (function () {
            function AccountingPage($scope, api) {
                var _this = this;
                this.$scope = $scope;
                this.api = api;
                this.eventSource = [];
                this.eventSources = [this.eventSource];
                this.calendarConfig = {
                    height: 450,
                    editable: true,
                    header: {
                        left: "month",
                        center: "title",
                        right: "today prev,next"
                    },
                    dayClick: function (day, ev) { return _this.dayClick(day, ev); },
                    eventDrop: function (event, duration, rollback) { return _this.eventDrop(event, duration, rollback); },
                    eventResize: function () {
                        var args = [];
                        for (var _i = 0; _i < arguments.length; _i++) {
                            args[_i - 0] = arguments[_i];
                        }
                        return console.log(args);
                    },
                };
                this.api.loadInMonth(moment()).then(function (dto) {
                    var events = accounting.addColorToEventObjects(dto.map(function (x) { return accounting.mapEntityToCalendar(x); }));
                    _this.eventSource = events;
                });
            }
            AccountingPage.prototype.dayClick = function (day, ev) {
            };
            AccountingPage.prototype.eventDrop = function (event, duration, rollback) {
            };
            AccountingPage.$inject = ["$scope", "api"];
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