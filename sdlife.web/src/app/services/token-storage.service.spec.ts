import { TestBed, inject } from '@angular/core/testing';
import { TokenStorage } from './token-storage.service';

describe('TokenStorageService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [TokenStorage]
        });
    });

    it('should ...', inject([TokenStorage], (service: TokenStorage) => {
        expect(service).toBeTruthy();
    }));
});
