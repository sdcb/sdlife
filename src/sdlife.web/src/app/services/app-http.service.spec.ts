import { TestBed, inject } from '@angular/core/testing';
import { AppHttpService } from './app-http.service';
import { Injectable } from '@angular/core';
import { ReadyState, Connection, ConnectionBackend } from "@angular/http";

@Injectable()
class CustomBackend extends ConnectionBackend {
    createConnection(request: any): Connection {
        return {
            readyState: ReadyState.Done, 
            request: request, 
            response: null
        };
    };
}

describe('AppHttpService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [AppHttpService]
        });
    });

    it('should ...', inject([AppHttpService, CustomBackend], (service: AppHttpService) => {
        expect(service).toBeTruthy();
    }));
});
