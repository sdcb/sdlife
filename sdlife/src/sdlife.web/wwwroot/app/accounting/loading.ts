/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class LoadingController {
        size: number;
        oldPromise: ng.IPromise<any>;
        promise: ng.IPromise<any>;

        show = false;

        $onChanges() {
            if (this.promise && this.promise != this.oldPromise) {
                this.oldPromise = this.promise;
                this.show = true;
                this.promise.finally(() => this.show = false);
            }
        }
    }

    module.component("loading", {
        controller: LoadingController,
        templateUrl: "/app/accounting/loading.html",
        controllerAs: "vm",
        bindings: {
            size: "<", 
            promise: "<"
        },
    });
}