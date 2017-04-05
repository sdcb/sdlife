import { TestBed, inject } from '@angular/core/testing';
import { TokenStorageService } from './token-storage.service';

describe('TokenStorageService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [TokenStorageService]
        });
    });

    it('should ...', inject([TokenStorageService], (service: TokenStorageService) => {
        expect(service).toBeTruthy();
    }));
});
