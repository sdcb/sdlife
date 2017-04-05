﻿import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http, ConnectionBackend, XHRBackend, RequestOptions } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { Router } from "@angular/router";

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login.component';
import { AccountingComponent } from './components/accounting.component';

import { AppHttpService } from './services/app-http.service';
import { TokenStorageService } from './services/token-storage.service';
import { PageHeaderComponent } from './components/common/page-header.component';
//import "hammerjs";
//import "moment";

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
        MaterialModule.forRoot(),
        FlexLayoutModule,
        BrowserAnimationsModule
    ],
    providers: [
        TokenStorageService,
        {
            provide: Http, 
            useFactory: (
                backend: XHRBackend,
                defaultOptions: RequestOptions,
                tokenStorage: TokenStorageService,
                router: Router) => {
                return new AppHttpService(backend, defaultOptions, tokenStorage, router)
            }, 
            deps: [XHRBackend, RequestOptions, TokenStorageService, Router]
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
