import { Http } from '@angular/http';
import { Component, OnInit } from '@angular/core';
import { MdSnackBar } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { TokenStorage } from '../services/token-storage.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginDto: LoginDto = {
        username: "",
        password: "",
        rememberMe: true
    };
    redirectUrl = "/";

    inRequest = false;

    constructor(
        private http: Http,
        private snackBar: MdSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private tokenStorage: TokenStorage) {
    }

    ngOnInit(): void {
        this.route.params.subscribe(x => {
            if (x["redirectUrl"]) {
                this.redirectUrl = x["redirectUrl"];
            }
        });
    }

    login() {
        this.inRequest = true;
        this.http
            .post("/Account/CreateToken", this.loginDto)
            .subscribe(x => {
                let json = x.json();
                this.tokenStorage.store(
                    json.token,
                    json.expiration,
                    json.refreshTime);
                this.router.navigateByUrl(this.redirectUrl);
            }, err => {
                this.snackBar.open("用户名或密码不正确", "错误", {
                    duration: 1500
                });
            }, () => {
                this.inRequest = false;
            });
    }
}

interface LoginDto {
    username: string;
    password: string;
    rememberMe: boolean;
}