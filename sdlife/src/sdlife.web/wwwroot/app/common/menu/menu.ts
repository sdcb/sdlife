namespace sdlife {
    let app = angular.module(consts.moduleName);

    class SdlifeMenuComponent {
        accountingMenu = [
            new MenuItem("我", ["Book"])
        ];
        menus = <Array<IMenuItem>>[
            new MenuFolder("记帐", this.accountingMenu)
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
                    return new MenuItem(user.email, ["BookFriend", {userId: user.id}]);
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
        toggle() {
        }

        onclick() {
            this.toggle();
        }
    }

    class MenuItemComponent {
        menu: IMenuItem;
    }

    app.component("sdlifeMenu", {
        templateUrl: "/app/common/menu/menu.html",
        controller: SdlifeMenuComponent,
        controllerAs: "vm",
    });

    app.component("menuFolder", {
        templateUrl: "/app/common/menu/menu-folder.html",
        controller: MenuFolderComponent,
        controllerAs: "vm",
        bindings: {
            menu: "<",
            isOpen: "<",
            toggle: "&"
        }
    });

    app.component("menuItem", {
        templateUrl: "/app/common/menu/menu-item.html",
        controller: MenuItemComponent,
        controllerAs: "vm",
        bindings: {
            menu: "<"
        }
    });

    interface IMenuItem {
        type: string,
        title: string,
        open?: boolean, 
        state?: Array<any>,
        subMenus?: IMenuItem[],
        visible: () => boolean
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
        constructor(public title: string, public state: Array<any>) {
            super();
        }
    }

    class MenuFolder extends MenuItemBase implements IMenuItem {
        open = true;
        type = "folder";
        constructor(public title: string, public subMenus: IMenuItem[]) {
            super();
        }

        hide() {
            return this.subMenus.length > 0;
        }
    }
}