import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from "@angular/core";
import { AccountingComponent } from './accounting.component';

describe('AccountingComponent', () => {
    let component: AccountingComponent;
    let fixture: ComponentFixture<AccountingComponent>;

    beforeEach(async(() => {
        TestBed
            .configureTestingModule({
                declarations: [AccountingComponent],
                schemas: [ NO_ERRORS_SCHEMA ]
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
