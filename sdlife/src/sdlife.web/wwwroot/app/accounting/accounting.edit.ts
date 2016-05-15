namespace sdlife.accounting {
    class AcountingEditDialog {
        $searchTitle = "";
        loading: ng.IPromise<any>;
        editTime: Date;

        commit(valid: boolean) {
            let time = moment(this.editTime);
            this.loading = this.api.update({
                id: this.input.id, 
                amount: this.input.amount,
                comment: this.input.comment,
                time: moment(this.editTime)
                    .hour(time.hour())
                    .minute(time.minute())
                    .second(moment().second())
                    .millisecond(moment().millisecond())
                    .format(),
                title: this.input.title || this.$searchTitle
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

        static $inject = ["$mdDialog", "entity", "api"];
        constructor(
            public dialog: ng.material.IDialogService,
            public input: IAccountingEntity,
            public api: AccountingApi
        ) {
            console.log(this);
            this.editTime = moment(this.input.time).startOf("minute").toDate();
        }
    }

    export function showAccountingEditDialog(
        entity: IAccountingEntity, 
        dialog: ng.material.IDialogService,
        ev: MouseEvent) {
        return dialog.show(<ng.material.IDialogOptions>{
            controller: AcountingEditDialog,
            templateUrl: `/app/accounting/accounting.edit.html?${consts.version}`, 
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev, 
            locals: {
                entity: entity
            }, 
        });
    }
    
    let module = angular.module(consts.moduleName);
}