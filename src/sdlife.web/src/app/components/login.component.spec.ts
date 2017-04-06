import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from "@angular/http";
import { MaterialModule } from "@angular/material"
import { FormsModule } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/empty";

import { LoginComponent } from './login.component';
import { TokenStorageService } from "../services/token-storage.service";


describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;

    beforeEach(async(() => {
        TestBed
            .configureTestingModule({
                declarations: [LoginComponent],
                imports: [HttpModule, MaterialModule, FormsModule], 
                providers: [
                    {
                        provide: Router,
                        useValue: null, 
                    }, 
                    {
                        provide: ActivatedRoute, 
                        useValue: {
                            params: Observable.empty()
                        }, 
                    }, 
                    TokenStorageService
                ]
            })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
