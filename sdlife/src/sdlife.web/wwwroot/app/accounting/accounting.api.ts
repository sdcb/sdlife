/// <reference path="accounting.api.d.ts" />
namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    export class AccountingApi {
        static $inject = ["$http"];
        constructor(public $: angular.IHttpService) {
        }

        loadInMonth(month: moment.Moment | string) {
            let time = moment(month);
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

        updateTime(id: number, time: string) {
            return this.$.post("/Accounting/UpdateTime", { time: time }).then(cb => {
                return cb.data;
            });
        }

        delete(id: number) {
            return this.$.post("/Accounting/Delete", { id: id });
        }

        get3() {
            return 3;
        }
    }

    module.service("api", AccountingApi);
}