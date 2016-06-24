namespace sdlife {
    let app = angular.module(consts.moduleName);

    class AppController {
        $router: ng.Router;
        loading: ng.IPromise<any>;

        showChangePasswordDialog(ev: MouseEvent) {
            login.showChangePasswordDialog(this.dialog, this.media, ev);
        }

        logout(ev: MouseEvent) {
            ensure(this.dialog, ev, "确定要注销登陆吗？").then(() => {
                return this.loading = this.loginApi.logout();
            }).then(() => {
                this.$router.navigate(["Login"]);
            });
        }

        static $inject = ["$mdDialog", "$mdMedia", "login.api"];
        constructor(
            public dialog: ng.material.IDialogService,
            public media: ng.material.IMedia,
            public loginApi: login.LoginApi) {
        }
    }

    app.component("sdlifeApp", {
        controller: AppController,
        controllerAs: "vm",
        templateUrl: "/app/common/root/app.html",
        bindings: {
            $router: "<",
        },
        $routeConfig: [
            { path: "/book/me", component: "accounting-page", name: "Book" },
            { path: "/book/:userId", component: "accounting-page-for-friend", name: "BookFriend" },
            { path: "/**", redirectTo: ["Book"] }
        ]
    });
}