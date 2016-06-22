namespace sdlife.login {
    let app = angular.module(consts.moduleName);

    class ChangePasswordDialog {
        loading: ng.IPromise<void>;
        oldPassword: string;
        password: string;
        confirmedPassword: string;

        cancel() {
            this.dialog.cancel();
        }

        commit() {
            if (this.password === this.confirmedPassword) {
                return this.loading = this.api
                    .changePassword(this.oldPassword, this.password)
                    .then(data => this.dialog.hide(data)).catch(() => {
                        this.toast.showSimple("修改密码失败");
                    });
            } else {
                return this.toast.showSimple("两次密码输入不相同");
            }
        }

        static $inject = ["$mdDialog", "login.api", "$mdToast"];
        constructor(
            public dialog: ng.material.IDialogService,
            public api: LoginApi,
            public toast: ng.material.IToastService) {
        }
    }

    export function showChangePasswordDialog(
        dialog: ng.material.IDialogService,
        media: ng.material.IMedia,
        ev: MouseEvent) {
        return dialog.show(<ng.material.IDialogOptions>{
            controller: ChangePasswordDialog,
            templateUrl: "/app/login/change-password.html",
            controllerAs: "vm",

            clickOutsideToClose: false,
            targetEvent: ev,
            fullscreen: isSmallDevice(media)
        });
    }
}