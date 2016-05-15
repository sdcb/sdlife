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
            ensure(this.dialog, event, "确定要删除此条数据吗？").then(() => {
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
        media: ng.material.IMedia, 
        onCommit: () => any) {
        let thisDialog: ng.material.IDialogOptions = {
            autoWrap: true, 
            controller: AcountingEditDialog,
            templateUrl: `/app/accounting/accounting.edit.html?${consts.version}`,
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev,
            locals: {
                entity: entity,
                onCommit: onCommit
            },
            fullscreen: isSmallDevice(media)
        };
        thisDialog.locals["thisDialog"] = thisDialog;
        return dialog.show(thisDialog);
    }

    let module = angular.module(consts.moduleName);
}