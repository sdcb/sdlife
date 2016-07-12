namespace sdlife.accounting {
    class AcountingCreateDialog {
        $searchTitle = "";
        loading: ng.IPromise<any>;
        result = {
            amount: 0,
            comment: "",
            time: moment().startOf("minute").toDate(),
            isIncome: false, 
            title: ""
        };

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
                isIncome: this.result.isIncome, 
                title: this.result.title || this.$searchTitle
            };
            return this.api.create(data, this.userId);
        }

        commit(valid: boolean) {
            this.loading = this.create()
                .then(data => this.dialog.hide(data));
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchAutoTitles(title, this.result.isIncome);
        }

        static $inject = ["$scope", "$mdDialog", "accounting.api", "date", "userId"];
        constructor(
            public scope: ng.IScope, 
            public dialog: ng.material.IDialogService,
            public api: AccountingApi, 
            public date: string, 
            public userId: number
        ) {
            scope.$watch(() => this.result.isIncome, () => {
                this.$searchTitle = "";
                this.result.title = "";
            });
        }
    }

    export function showAccountingCreateDialog(
        date: string, 
        userId: number, 
        dialog: ng.material.IDialogService,
        media: ng.material.IMedia, 
        ev: MouseEvent) {
        return dialog.show(<ng.material.IDialogOptions>{
            controller: AcountingCreateDialog,
            templateUrl: "/app/accounting/accounting.create.html", 
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev, 
            locals: {
                date: date, 
                userId: userId
            }, 
            fullscreen: isSmallDevice(media)
        });
    }
    
    let module = angular.module(consts.moduleName);
}