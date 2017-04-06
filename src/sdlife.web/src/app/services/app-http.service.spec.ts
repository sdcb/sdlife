import { TestBed, inject } from '@angular/core/testing';
import { AppHttpService } from './app-http.service';
import { TokenStorageService } from "./token-storage.service";
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

class MyTokenStorageService extends TokenStorageService {

}

describe('AppHttpService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                AppHttpService,
                {
                    provide: ConnectionBackend, 
                    useValue: new CustomBackend()
                }, 
                {
                    provide: RequestOptions, 
                    useValue: null, 
                }, 
                {
                    provide: TokenStorageService, 
                    useValue: new MyTokenStorageService()
                }, 
                {
                    provide: Router, 
                    useValue: new MyTokenStorageService()
                }
            ]
        });
    });

    it('should ...', inject([AppHttpService], (service: AppHttpService) => {
        expect(service).toBeTruthy();
    }));
});
