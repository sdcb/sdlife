/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.login {
    let app = angular.module(consts.moduleName);

    export class LoginApi {
        static $inject = ["$http"];
        constructor(public $: ng.IHttpService) {
        }

        login(dto: ILoginDto) {
            return this.$.post("/Account/Login", dto);
        }

        refreshCsrf() {
            return this.$.post("/Account/RefreshCsrf", {}).then(resp => {
                return resp.data;
            });
        }
    }

    export interface ILoginDto {
        userName: string;
        password: string;
        rememberMe: boolean;
    }

    app.service("login.api", LoginApi);
}