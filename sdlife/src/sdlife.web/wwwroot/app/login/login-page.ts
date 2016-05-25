/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.login {
    let module = angular.module(consts.moduleName);

    class LoginPage {

    }

    module.component("loginPage", {
        controller: LoginPage,
        controllerAs: "vm",
        templateUrl: "/app/login/login-page.html",
    });
}