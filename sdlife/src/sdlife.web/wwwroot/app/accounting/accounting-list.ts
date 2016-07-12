/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingList extends AccountingBasePage {
        data: IPagedList<IAccountingDto>;
        query: IAccountingPagedListQuery = {
            page: 1, 
            pageSize: 12, 
            userId: 0, 
            orderBy: "-Time", 
        };
        promise: ng.IPromise<any>;

        $routerOnActivate(next) {
            super.$routerOnActivate(next);
            this.loadData();
            this.query.userId = this.userId;
        }

        loadData = () => {
            return this.promise = this.api.loadList(this.query).then(data => this.data = data);
        }

        onCreated() {
            this.loadData();
        }

        searchTitle(title: string) {
            return this.api.searchSpendingTitles(title);
        }

        static $inject = ["accounting.api", "$scope"];
        constructor(
            api: AccountingApi,
            scope: ng.IScope) {
            super(api);

            scope.$watch(() => this.query.title, (newValue: string, oldValue: string) => {
                if (newValue !== oldValue) this.loadData();
            });
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