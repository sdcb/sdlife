/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingList extends AccountingBasePage {
        data: IPagedList<IAccountingDto>;
        query: ISqlPagedListQuery = {
            page: 1, 
            pageSize: 12, 
            sql: "", 
            orderBy: "-Time", 
        };
        queryOk = true;
        promise: ng.IPromise<any>;

        $routerOnActivate(next) {
            super.$routerOnActivate(next);
            this.loadData();
        }

        loadData = () => {
            return this.promise = this.api.loadSqlList(this.query)
                .then(data => this.data = data)
                .then(() => this.queryOk = true)
                .catch(() => this.queryOk = false);
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

            scope.$watch(() => this.query.sql, (newValue: string, oldValue: string) => {
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