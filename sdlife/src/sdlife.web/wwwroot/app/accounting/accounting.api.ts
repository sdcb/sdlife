/// <reference path="accounting.api.d.ts" />
namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    export class AccountingApi {
        static $inject = ["$http"];
        constructor(public $: angular.IHttpService) {
            $.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
        }

        loadInMonth(month: moment.Moment) {
            let time = month.clone();
            let from = time.clone().startOf("month");
            let to = from.clone().add(1, "month");

            return this.$.post<IAccountingEntity[]>("/Accounting/MyAccountingInRange", {
                from: from,
                to: to
            }).then(cb => {
                return cb.data;
            });
        }

        create(dto: IAccountingDto) {
            return this.$.post<IAccountingEntity>("/Accounting/Create", dto).then(cb => {
                return cb.data;
            });
        }

        update(entity: IAccountingEntity) {
            return this.$.post<IAccountingEntity>("/Accounting/Update", entity).then(cb => {
                return cb.data;
            });
        }

        updateTime(id: number, time: string | moment.Moment) {
            return this.$.post(`/Accounting/UpdateTime?id=${id}`, time);
        }

        delete(id: number) {
            return this.$.post(`/Accounting/Delete?id=${id}`, {});
        }

        searchSpendingTitles(title: string) {
            return this.$.post<string[]>("/Accounting/searchSpendingTitles", { query: title }).then(cb => {
                return cb.data;
            });
        }

        searchIncomeTitles(title: string) {
            return this.$.post<string[]>("/Accounting/searchIncomeTitles", { query: title }).then(cb => {
                return cb.data;
            });
        }

        searchAutoTitles(title: string, isIncome: boolean) {
            return isIncome ?
                this.searchIncomeTitles(title) :
                this.searchSpendingTitles(title);
        }
    }

    module.service("api", AccountingApi);
}