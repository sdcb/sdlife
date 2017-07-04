import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService, AccountingDto, AccountingEntity } from '../services/data.service';
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
    editDialog: AccountingCreateDialog;

    constructor(
        private router: Router,
        private data: DataService) {
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        let now = moment();
        let currentMonth = now.clone().startOf("month");
        this.accountings = this.data.loadAccountingInRange(
            formatDate(currentMonth),
            formatDate(now), null);
    }

    edit() {
        console.log("editing...");
    }

    async delete(id) {
        $("#table_id").val(id);
    }

    async confirm() {
        let id = $("#table_id").val();
        let response = await this.data.deleteAccounting(id).toPromise();
        $("#delete-dialog").modal("hide");
        this.loadData();
    }

    async create() {
        var data = this.createDialog;
        if (data.id != undefined) {
            let response = await this.data.editAccounting(this.createDialog.editDto()).toPromise();
        } else {
            let response = await this.data.createAccounting(this.createDialog.createDto()).toPromise();
        }
        
        $("#create-dialog").modal("hide");
        this.loadData();
    }

    openCreateDialog() {
        this.createDialog = new AccountingCreateDialog();
    }

    openEditDialog(v: AccountingEntity) {
        let item = new AccountingCreateDialog();
        item.id = v.id || -1;
        item.title = v.title;
        item.amount = v.amount;
        item.comment = v.comment || "";
        this.createDialog = item;
        console.log(item);
    }
}

class AccountingCreateDialog {
    id: number;
    isIncome = false;
    time = moment().format("L");
    title: string;
    amount = 0;
    comment: string;

    createDto(): AccountingDto {
        let time = moment(this.time);
        let timePart = moment().diff(moment().startOf("day"));
        let dateTime = time.add(timePart);
        return {
            isIncome: this.isIncome,
            time: formatDate(dateTime),
            title: this.title,
            amount: this.amount,
            comment: this.comment
        }
    }

    editDto(): AccountingEntity {
        let time = moment(this.time);
        let timePart = moment().diff(moment().startOf("day"));
        let dateTime = time.add(timePart); 
        return {
            id: this.id,
            isIncome: this.isIncome,
            time: formatDate(dateTime),
            title: this.title,
            amount: this.amount,
            comment:this.comment
        }

    }


}

function formatDate(d: moment.Moment) {
    return d.format("YYYY-MM-DDTHH:mm:ss.SSSSSSS");
}