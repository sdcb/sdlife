﻿namespace sdlife {
    let app = angular.module(consts.moduleName);

    class SdlifeMenuComponent {
        accountingFriendSubMenus = new MenuFolder("我的朋友", []);
        menus = <Array<IMenuItem>>[
            new MenuItem("我", ["Book"]),
            this.accountingFriendSubMenus
        ];

        currentFolder: IMenuItem;

        isFolderOpen(folder: IMenuItem) {
            return this.currentFolder === folder;
        }

        toggleFolder(folder: IMenuItem) {
            console.log(folder);
            if (folder !== this.currentFolder) {
                this.currentFolder = folder;
            } else {
                this.currentFolder = null;
            }
        }

        setupAccountingUsers() {
            this.api.authorizedUsers().then(users => {
                this.accountingFriendSubMenus.subMenus = users.map(user => {
                    return new MenuItem(user.email, ["BookFriend", {userId: user.id}]);
                });
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
        type = "folder";
        constructor(public title: string, public subMenus: IMenuItem[]) {
            super();
        }

        hide() {
            return this.subMenus.length > 0;
        }
    }
}