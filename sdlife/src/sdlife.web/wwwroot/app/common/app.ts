namespace sdlife {
    export var consts = {
        moduleName: "sdlife",
        version: new Date().getTime(),
    };

    let module = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar", "ngMessages"]);
}