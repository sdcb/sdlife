﻿import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http, ConnectionBackend, XHRBackend, RequestOptions } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { Router } from "@angular/router";

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login.component';
import { AccountingComponent } from './components/accounting.component';

import { AppHttp } from './services/app-http.service';
import { TokenStorage } from './services/token-storage.service';
import { PageHeaderComponent } from './components/common/page-header.component';
import "bootstrap";
//import "hammerjs";

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        AccountingComponent,
        PageHeaderComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        BrowserAnimationsModule
    ],
    providers: [
        TokenStorage,
        {
            provide: AppHttp,
            useFactory(backend, defaultOptions, tokenStorage, router) {
                return new AppHttp(backend, defaultOptions, tokenStorage, router)
            },
            deps: [XHRBackend, RequestOptions, TokenStorage, Router]
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
