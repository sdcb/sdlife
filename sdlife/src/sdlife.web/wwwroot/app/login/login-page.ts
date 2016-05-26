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
        prevPage: ng.ComponentInstruction;
        $router: ng.Router;

        commit() {
            this.loading = this.api.login(this.input).catch(() => {
                this.$mdToast.show(this.$mdToast.simple()
                    .textContent("用户名或密码错误!")
                    .position("top right")
                    .hideDelay(3000));
            }).then(() => {
                this.goPrevPage();
            });
        }

        goPrevPage() {
            if (this.prevPage) {
                return this.$router.navigateByUrl(this.prevPage.urlPath);
            } else {
                this.$router.navigateByUrl("/");
            }
        }

        $routerOnActivate(next, prevPage: ng.ComponentInstruction) {
            this.prevPage = prevPage;
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
        templateUrl: "/app/login/login-page.html",
        bindings: {
            $router: "<"
        }
    });
}