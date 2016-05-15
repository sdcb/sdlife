namespace sdlife.accounting {
    class AcountingCreateDialog {
        result = {
            amount: 0, 
            comment: null, 
            time: moment().startOf("minute").toDate(), 
            title: ""
        };
        $searchTitle = "";
        size = 90;
        loading: ng.IPromise<any>;

        commit(valid: boolean) {
            let time = moment(this.result.time);
            this.loading = this.api.create({
                amount: this.result.amount,
                comment: this.result.comment,
                time: moment(this.date)
                    .hour(time.hour())
                    .minute(time.minute())
                    .second(moment().second())
                    .millisecond(moment().millisecond())
                    .format(),
                title: this.result.title || this.$searchTitle
            }).then((data) => {
                this.dialog.hide(data);
            });
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchTitle(title);
        }

        static $inject = ["$mdDialog", "date", "api", "$timeout"];
        constructor(
            public dialog: ng.material.IDialogService,
            public date: string,
            public api: AccountingApi, 
            public timeout: ng.ITimeoutService
        ) {
            console.log(this);
            timeout(() => this.size = 150, 2000);
        }
    }

    export function showAccountingCreateDialog(
        date: string, 
        dialog: ng.material.IDialogService,
        ev: MouseEvent) {
        return dialog.show(<ng.material.IDialogOptions>{
            controller: AcountingCreateDialog,
            templateUrl: `/app/accounting/accounting.create.html?${consts.version}`, 
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev, 
            locals: {
                date: date
            }, 
        });
    }
    
    let module = angular.module(consts.moduleName);
}