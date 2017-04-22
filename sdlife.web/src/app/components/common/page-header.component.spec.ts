import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from "@angular/router";

import { PageHeaderComponent } from './page-header.component';
import { TokenStorage } from "../../services/token-storage.service";

describe('PageHeaderComponent', () => {
    let component: PageHeaderComponent;
    let fixture: ComponentFixture<PageHeaderComponent>;

    beforeEach(async(() => {
        TestBed
            .configureTestingModule({
                declarations: [PageHeaderComponent], 
                imports: [], 
                providers: [
                    TokenStorage, 
                    {
                        provide: Router, 
                        useValue: null, 
                    }
                ]
            })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PageHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
