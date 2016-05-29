namespace sdlife {
    let app = angular.module(consts.moduleName);

    class SdlifeMenuComponent {
    }

    app.component("sdlifeMenu", {
        templateUrl: "/app/common/menu/menu.html", 
        controller: SdlifeMenuComponent, 
        controllerAs: "vm", 
    });
}