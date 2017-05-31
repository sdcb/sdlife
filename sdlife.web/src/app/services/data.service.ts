import { Injectable } from '@angular/core';
import { AppHttp } from "../services/app-http.service";

@Injectable()
export class DataService {
    constructor(private http: AppHttp) { }

    loadAccountingInRange(from: string, to: string, userId: number | null) {
        let query = {
            from: from,
            to: to,
            userId: userId
        };
        return this.http
            .post("/Accounting/Get", query)
            .map(res => <AccountingDto[]>res.json());
    }

    createAccounting(data: AccountingDto) {
        let dto: AccountingDto = {
            title: data.title,
            amount: data.amount,
            isIncome: data.isIncome,
            comment: data.comment,
            time: data.time
        };
        return this.http
            .post("/Accounting/Create", dto);
    }

    editAccounting(data: AccountingDto) {
        let dto: AccountingDto = {
            id: data.id,
            title: data.title,
            amount: data.amount,
            isIncome: data.isIncome,
            comment: data.comment,
            time: data.time
        };
        return this.http
            .post("/Accounting/Update", dto);
    }

    deleteAccounting(id) {
        return this.http
            .post(`/Accounting/Delete?id=${id}`, null);
    }
}

export interface AccountingEntity extends AccountingDto {
    id: number;
}

export interface AccountingDto {
    id?: number;
    title: string;
    amount: number;
    time: string;
    isIncome: boolean;
    comment?: string;
}

export interface PagedListQuery {
    page?: number;
    pageSize?: number;
    orderBy?: string;
}

export interface SqlPagedListQuery extends PagedListQuery {
    sql: string;
}

export interface AccountingPagedListQuery extends PagedListQuery {
    userId: number | null;
    title?: string;
    titles?: string[];
    from?: string;
    to?: string;
    isIncome?: boolean;
    minAmount?: number;
    maxAmount?: number;
}

export interface PagedList<T> {
    totalCount: number;
    items: T[];
}