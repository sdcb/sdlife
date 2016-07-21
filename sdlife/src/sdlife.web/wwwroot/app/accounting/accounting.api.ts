/// <reference path="accounting.api.d.ts" />
namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    export class AccountingApi {
        static $inject = ["$http"];
        constructor(public $: angular.IHttpService) {
        }

        loadInRange(from: string, to: string, userId: number | null) {
            return this.$.post<IAccountingEntity[]>("/Accounting/Get", {
                from: from,
                to: to,
                userId: userId
            }).then(cb => {
                return NN(cb.data);
            });
        }

        loadList(query: IAccountingPagedListQuery): ng.IPromise<IPagedList<IAccountingDto>> {
            return this.$.post("/Accounting/List", query)
                .then(cb => NN(cb.data));
        }

        create(dto: IAccountingDto, userId: number) {
            return this.$.post<IAccountingEntity>(`/Accounting/Create?userId=${userId}`, dto).then(cb => {
                return NN(cb.data);
            });
        }

        update(entity: IAccountingEntity) {
            return this.$.post<IAccountingEntity>("/Accounting/Update", entity).then(cb => {
                return NN(cb.data);
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
                return NN(cb.data);
            });
        }

        searchIncomeTitles(title: string) {
            return this.$.post<string[]>("/Accounting/searchIncomeTitles", { query: title }).then(cb => {
                return NN(cb.data);
            });
        }

        searchAutoTitles(title: string, isIncome: boolean) {
            return isIncome ?
                this.searchIncomeTitles(title) :
                this.searchSpendingTitles(title);
        }

        authorizedUsers() {
            return this.$.post<Array<IAccountingAuthorizedUser>>("/Accounting/AuthorizedUsers", {}).then(cb => {
                return NN(cb.data);
            });
        }

        canIModify(userId: number) {
            return this.$.post<boolean>(`/Accounting/CanIModify?userId=${userId}`, {}).then(cb => {
                return NN(cb.data);
            });
        }
    }

    module.service("accounting.api", AccountingApi);
}