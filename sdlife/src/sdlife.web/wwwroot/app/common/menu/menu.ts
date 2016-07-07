namespace sdlife {
    let app = angular.module(consts.moduleName);

    class SdlifeMenuComponent {
        router: ng.Router;

        accountingMenu = [
            new MenuItem("我", ["BookCalendar", { userId: "me" }], this.router)
        ];
        menus = <Array<IMenuItem>>[
            new MenuFolder("记帐", this.accountingMenu, this.router)
        ];

        isFolderOpen(folder: IMenuItem) {
            return folder.open;
        }

        toggleFolder(folder: IMenuItem) {
            folder.open = !folder.open;
        }

        setupAccountingUsers() {
            this.api.authorizedUsers().then(users => {
                this.accountingMenu.push(...users.map(user => {
                    return new MenuItem(user.email, ["BookCalendar", { userId: user.id }], this.router);
                }));
            });
        }

        static $inject = ["accounting.api"];
        constructor(public api: accounting.AccountingApi) {
            this.setupAccountingUsers();
        }
    }

    class MenuFolderComponent {
        isOpen: boolean;
        menu: IMenuItem;
        router: ng.Router;
        toggle() {
        }

        isActive() {
            return this.menu.isActive();
        }

        onclick() {
            this.toggle();
        }
    }

    class MenuItemComponent {
        menu: IMenuItem;

        isActive() {
            return this.menu.isActive();
        }
    }

    app.component("sdlifeMenu", {
        templateUrl: "/app/common/menu/menu.html",
        controller: SdlifeMenuComponent,
        controllerAs: "vm",
        bindings: {
            router: "<",
        }
    });

    app.component("menuFolder", {
        templateUrl: "/app/common/menu/menu-folder.html",
        controller: MenuFolderComponent,
        controllerAs: "vm",
        bindings: {
            menu: "<",
            isOpen: "<",
            toggle: "&",
        }
    });

    app.component("menuItem", {
        templateUrl: "/app/common/menu/menu-item.html",
        controller: MenuItemComponent,
        controllerAs: "vm",
        bindings: {
            menu: "<",
        }
    });

    interface IMenuItem {
        type: string,
        title: string,
        open?: boolean,
        state?: Array<any>,
        subMenus?: IMenuItem[],
        visible(): boolean,
        isActive(): boolean, 
    }

    class MenuItemBase {
        hide() {
            return false;
        }

        visible() {
            if (this.hide === undefined) {
                return true;
            } else if (typeof this.hide === "function") {
                return (<Function>this.hide)();
            } else {
                return this.hide;
            }
        }
    }

    class MenuItem extends MenuItemBase implements IMenuItem {
        type = "item";
        constructor(public title: string, public state: Array<any>, public router: ng.Router) {
            super();
        }

        isActive() {
            return this.router.isRouteActive(this.router.generate(this.state));
        }
    }

    class MenuFolder extends MenuItemBase implements IMenuItem {
        open = true;
        type = "folder";
        constructor(public title: string, public subMenus: IMenuItem[], public router: ng.Router) {
            super();
        }

        hide() {
            return this.subMenus.length > 0;
        }

        isActive() {
            return this.subMenus.some(v => v.isActive());
        }
    }
}