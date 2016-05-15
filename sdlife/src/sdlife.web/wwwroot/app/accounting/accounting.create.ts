namespace sdlife.accounting {
    class AcountingCreateDialog {
        result = {
            amount: 0, 
            comment: null, 
            time: moment().startOf("minute").toDate(), 
            title: ""
        };
        $searchTitle = "";

        commit(valid: boolean) {
            console.log(valid);
        }

        answer() {
            this.dialog.hide();
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchTitle(title);
        }

        static $inject = ["$mdDialog", "date", "api"];
        constructor(
            public dialog: ng.material.IDialogService,
            public date: moment.Moment,
            public api: AccountingApi
        ) {
            console.log(this);
        }
    }

    export function showAccountingCreateDialog(
        date: moment.Moment, 
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