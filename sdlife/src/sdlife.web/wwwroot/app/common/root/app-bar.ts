namespace sdlife {
    class AppBarComponent {
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

        openMenu() {
            this.sidenav("left").toggle();
        }

        static $inject = ["$mdDialog", "$mdMedia", "login.api", "$mdSidenav"];
        constructor(
            public dialog: ng.material.IDialogService,
            public media: ng.material.IMedia,
            public loginApi: login.LoginApi,
            public sidenav: ng.material.ISidenavService) {
        }
    }

    let app = angular.module(consts.moduleName);
    app.component("appBar", {
        controller: AppBarComponent, 
        controllerAs: "vm", 
        templateUrl: "/app/common/root/app-bar.html", 
        transclude: true, 
        bindings: {
            "$router": "<"
        }
    });
}