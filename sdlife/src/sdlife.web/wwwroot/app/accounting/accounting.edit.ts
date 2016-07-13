namespace sdlife.accounting {
    class AcountingEditDialog {
        $searchTitle = "";
        loading: ng.IPromise<any>;
        editTime: Date;

        update() {
            let time = moment(this.editTime);
            let data = {
                id: this.result.id,
                amount: this.result.amount,
                comment: this.result.comment,
                time: moment(this.editTime)
                    .hour(time.hour())
                    .minute(time.minute())
                    .second(moment().second())
                    .millisecond(moment().millisecond())
                    .format(),
                isIncome: this.result.isIncome, 
                title: this.result.title || this.$searchTitle
            };
            return this.api.update(data);
        }

        commit(valid: boolean) {
            this.loading = this.update().then((data) => {
                this.dialog.hide(data);
                this.onCommit();
            });
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchAutoTitles(title, this.result.isIncome);
        }

        delete(event: MouseEvent) {
            ensure(this.dialog, event, "确定要删除此条数据吗？").then(() => {
                return this.api.delete(this.result.id);
            }).then(() => {
                this.onCommit();
            }).catch(() => {
                this.dialog.show(this.me);
            });
        }

        static $inject = ["$mdDialog", "entity", "accounting.api", "thisDialog", "onCommit"];
        constructor(
            public dialog: ng.material.IDialogService,
            public result: IAccountingEntity,
            public api: AccountingApi,
            public me: ng.material.IDialogOptions,
            public onCommit: () => any
        ) {
            this.editTime = moment(this.result.time).startOf("minute").toDate();
        }
    }

    export function showAccountingEditDialog(
        entity: IAccountingEntity,
        dialog: ng.material.IDialogService,
        ev: MouseEvent,
        media: ng.material.IMedia, 
        onCommit: () => any) {
        let thisDialog: ng.material.IDialogOptions = {
            autoWrap: true, 
            controller: AcountingEditDialog,
            templateUrl: "/app/accounting/accounting.edit.html",
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev,
            locals: {
                entity: entity,
                onCommit: onCommit
            },
            fullscreen: isSmallDevice(media)
        };
        
        if (thisDialog.locals) {
            thisDialog.locals["thisDialog"] = thisDialog;
        } else {
            NN(thisDialog.locals);
        }
        
        return dialog.show(thisDialog);
    }

    let module = angular.module(consts.moduleName);
}