import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from "@angular/http";
import { Router } from "@angular/router";
import { MaterialModule } from "@angular/material";

import { AccountingComponent } from './accounting.component';
import { PageHeaderComponent } from "./common/page-header.component";
import { TokenStorageService } from "../services/token-storage.service";


describe('AccountingComponent', () => {
    let component: AccountingComponent;
    let fixture: ComponentFixture<AccountingComponent>;

    beforeEach(async(() => {
        TestBed
            .configureTestingModule({
                declarations: [AccountingComponent, PageHeaderComponent],
                imports: [HttpModule, MaterialModule], 
                providers: [
                    {
                        provide: Router, 
                        useValue: null, 
                    }, 
                    TokenStorageService
                ]
            })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AccountingComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
