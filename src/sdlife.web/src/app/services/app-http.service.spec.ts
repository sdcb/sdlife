import { TestBed, inject } from '@angular/core/testing';
import { AppHttp } from './app-http.service';
import { TokenStorage } from "./token-storage.service";
import { Injectable } from '@angular/core';
import { RequestOptions, HttpModule, ReadyState, Connection, ConnectionBackend } from "@angular/http";
import { Router } from "@angular/router"

class CustomBackend extends ConnectionBackend {
    createConnection(request: any): Connection {
        return {
            readyState: ReadyState.Done, 
            request: request, 
            response: null
        };
    };
}

class MyTokenStorageService extends TokenStorage {

}

describe('AppHttpService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                AppHttp,
                {
                    provide: ConnectionBackend, 
                    useValue: new CustomBackend()
                }, 
                {
                    provide: RequestOptions, 
                    useValue: null, 
                }, 
                {
                    provide: TokenStorage, 
                    useValue: new MyTokenStorageService()
                }, 
                {
                    provide: Router, 
                    useValue: new MyTokenStorageService()
                }
            ]
        });
    });

    it('should ...', inject([AppHttp], (service: AppHttp) => {
        expect(service).toBeTruthy();
    }));
});
