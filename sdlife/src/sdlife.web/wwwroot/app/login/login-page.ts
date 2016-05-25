/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.login {
    let module = angular.module(consts.moduleName);

    class LoginPage {
        input = {
            username: "",
            password: ""
        };

        commit() {
            console.log(this.input);
        }

        static $inject = ["$http"];
        constructor($http: ng.IHttpService) {   
            
        }
    }

    module.component("loginPage", {
        controller: LoginPage,
        controllerAs: "vm",
        templateUrl: `/app/login/login-page.html?v=${consts.version}`,
    });
}