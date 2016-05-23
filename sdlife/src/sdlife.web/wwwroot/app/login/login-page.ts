/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.login {
    let module = angular.module(consts.moduleName);

    module.component("loginPage", {
        controller: function () { },
        controllerAs: "vm",
        templateUrl: "/app/login/login-page.html",
    });
}