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
            this.loading = this.api
                .changePassword(this.oldPassword, this.password)
                .then(data => this.dialog.hide(data));
        }

        static $inject = ["$dialog", "login.api"];
        constructor(
            public dialog: ng.material.IDialogService,
            public api: LoginApi) {
        }
    }

    export function showChangePasswordDialog(
        date: string,
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