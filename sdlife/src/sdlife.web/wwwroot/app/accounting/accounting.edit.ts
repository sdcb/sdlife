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
                this.onCommit();
            });
        }

        delete(event: MouseEvent) {
            this.dialog.show(this.dialog.confirm()
                .title("确定要删除此条数据吗？")
                .ok("确认")
                .cancel("取消")
                .clickOutsideToClose(true)
                .targetEvent(event)
                .parent(event.srcElement)
            ).then(() => {
                return this.api.delete(this.input.id);
            }).then(() => {
                this.onCommit();
            }).catch(() => {
                this.dialog.show(this.me);
            });
        }

        cancel() {
            this.dialog.cancel();
        }

        searchTitle(title: string) {
            return this.api.searchTitle(title);
        }

        static $inject = ["$mdDialog", "entity", "api", "thisDialog", "onCommit"];
        constructor(
            public dialog: ng.material.IDialogService,
            public input: IAccountingEntity,
            public api: AccountingApi,
            public me: ng.material.IDialogOptions,
            public onCommit: () => any
        ) {
            this.editTime = moment(this.input.time).startOf("minute").toDate();
        }
    }

    export function showAccountingEditDialog(
        entity: IAccountingEntity,
        dialog: ng.material.IDialogService,
        ev: MouseEvent,
        onCommit: () => any) {
        let thisDialog: ng.material.IDialogOptions = {
            controller: AcountingEditDialog,
            templateUrl: `/app/accounting/accounting.edit.html?${consts.version}`,
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev,
            locals: {
                entity: entity,
                onCommit: onCommit
            },
        };
        thisDialog.locals["thisDialog"] = thisDialog;
        console.log(thisDialog);
        return dialog.show(thisDialog);
    }

    let module = angular.module(consts.moduleName);
}