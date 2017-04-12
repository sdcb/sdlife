﻿import { TestBed, inject } from '@angular/core/testing';
import { DataService } from './data.service';
import { HttpModule } from "@angular/http";

describe('DataService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [DataService], 
            imports: [HttpModule]
        });
    });

    it('should ...', inject([DataService], (service: DataService) => {
        expect(service).toBeTruthy();
    }));
});
