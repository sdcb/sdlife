/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.login {
    let app = angular.module(consts.moduleName);

    class LoginPage {
        input = {
            userName: "",
            password: "", 
            rememberMe: true, 
        };

        loading: ng.IPromise<any>;

        commit() {
            this.loading = this.api.login(this.input).then(() => {
            }).catch(() => {
                this.$mdToast.show(this.$mdToast.simple()
                    .textContent("用户名或密码错误!")
                    .position("top right")
                    .hideDelay(3000));
            });
        }

        static $inject = ["login.api", "$mdToast"];
        constructor(
            public api: LoginApi,
            public $mdToast: ng.material.IToastService) {
        }
    }

    app.component("loginPage", {
        controller: LoginPage,
        controllerAs: "vm",
        templateUrl: `/app/login/login-page.html?v=${consts.version}`,
    });
}