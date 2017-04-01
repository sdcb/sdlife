import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from "../../services/token-storage.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-page-header',
    templateUrl: './page-header.component.html',
    styleUrls: ['./page-header.component.css']
})
export class PageHeaderComponent implements OnInit {

    constructor(
        private tokenStorage: TokenStorageService,
        private router: Router) { }

    ngOnInit() {
    }

    signOff() {
        this.tokenStorage.removeToken();
        this.router.navigate(["login"]);
    }

}