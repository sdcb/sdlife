import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService, AccountingDto } from '../services/data.service';
import { Observable } from 'rxjs/Rx';
import * as moment from "moment";

@Component({
    selector: 'app-accounting',
    templateUrl: './accounting.component.html',
    styleUrls: ['./accounting.component.css'],
    providers: [DataService]
})
export class AccountingComponent implements OnInit {
    accountings: Observable<AccountingDto[]>;
    createDialog: AccountingCreateDialog;

    constructor(
        private router: Router,
        private data: DataService) {
    }

    ngOnInit() {
        let now = moment();
        let currentMonth = now.clone().startOf("month");
        this.accountings = this.data.loadAccountingInRange(currentMonth.toISOString(), now.toISOString(), null);
    }

    create() {
        
    }

    openCreateDialog() {
        this.createDialog = new AccountingCreateDialog();
    }
}

class AccountingCreateDialog {
    isIncome = false;
}
