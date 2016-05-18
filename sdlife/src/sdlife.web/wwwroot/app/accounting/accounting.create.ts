namespace sdlife.accounting {
    class AcountingCreateDialog {
        $searchTitle = "";
        loading: ng.IPromise<any>;
        isIncome = false;

        create() {
            let time = moment(this.result.time);
            let data = {
                amount: this.result.amount,
                comment: this.result.comment,
                time: moment(this.date)
                    .hour(time.hour())
                    .minute(time.minute())
                    .second(moment().second())
                    .millisecond(moment().millisecond())
                    .format(),
                title: this.result.title || this.$searchTitle
            };

            if (this.isIncome) {
                return this.api.createIncome(data);
            } else {
                return this.api.createSpend(data);
            }
        }

        commit(valid: boolean) {
            this.loading = this.create()
                .then(data => this.dialog.hide(data));
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchTitle(title);
        }

        result = {
            amount: 0,
            comment: null,
            time: moment().startOf("minute").toDate(),
            title: ""
        };

        static $inject = ["$mdDialog", "date", "api"];
        constructor(
            public dialog: ng.material.IDialogService,
            public date: string,
            public api: AccountingApi
        ) {
            console.log(this);
        }
    }

    export function showAccountingCreateDialog(
        date: string, 
        dialog: ng.material.IDialogService,
        media: ng.material.IMedia, 
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
            fullscreen: isSmallDevice(media)
        });
    }
    
    let module = angular.module(consts.moduleName);
}