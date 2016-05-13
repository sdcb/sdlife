/// <reference path="accounting.api.d.ts" />
var sdlife;
(function (sdlife) {
    var accounting;
    (function (accounting) {
        var module = angular.module(accounting.consts.moduleName);
        var AccountingApi = (function () {
            function AccountingApi($) {
                this.$ = $;
            }
            AccountingApi.prototype.loadInMonth = function (month) {
                var time = moment(month);
                var from = time.clone().startOf("month");
                var to = from.clone().add(1, "month");
                return this.$.post("/Accounting/MyAccountingInRange", {
                    from: from.format(),
                    to: to.format()
                }).then(function (cb) {
                    return cb.data;
                });
            };
            AccountingApi.prototype.create = function (dto) {
                return this.$.post("/Accounting/Create", dto).then(function (cb) {
                    return cb.data;
                });
            };
            AccountingApi.prototype.update = function (entity) {
                return this.$.post("/Accounting/Update", entity).then(function (cb) {
                    return cb.data;
                });
            };
            AccountingApi.prototype.updateTime = function (id, time) {
                return this.$.post("/Accounting/UpdateTime", { time: time }).then(function (cb) {
                    return cb.data;
                });
            };
            AccountingApi.prototype.delete = function (id) {
                return this.$.post("/Accounting/Delete", { id: id });
            };
            AccountingApi.prototype.get3 = function () {
                return 3;
            };
            AccountingApi.$inject = ["$http"];
            return AccountingApi;
        }());
        accounting.AccountingApi = AccountingApi;
        module.service("api", AccountingApi);
    })(accounting = sdlife.accounting || (sdlife.accounting = {}));
})(sdlife || (sdlife = {}));
//# sourceMappingURL=accounting.api.js.map