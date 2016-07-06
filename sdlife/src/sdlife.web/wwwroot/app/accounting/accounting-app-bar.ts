namespace sdlife {
    class AccountingAppBar extends AppBar {
        currentNavItem: string;

        getCurrentNavItemByUrl(url: string) {
            if (url.indexOf("calendar") !== -1) {
                return "calendar";
            } else if (url.indexOf("list") !== -1) {
                return "list";
            };
        }

        static $inject = ["$mdDialog", "$mdMedia", "login.api", "$mdSidenav", "$location"];
        constructor(
            dialog: ng.material.IDialogService,
            media: ng.material.IMedia,
            loginApi: login.LoginApi,
            sidenav: ng.material.ISidenavService,
            public location: ng.ILocationService) {
            super(dialog, media, loginApi, sidenav);
            this.currentNavItem = this.getCurrentNavItemByUrl(location.url());
        }
    }

    let app = angular.module(consts.moduleName);
    app.component("accountingAppBar", {
        controller: AccountingAppBar, 
        controllerAs: "vm", 
        templateUrl: "/app/accounting/accounting-app-bar.html", 
        bindings: {
            "router": "<"
        }
    });
}