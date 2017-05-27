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
        console.log(data);
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
}

export interface AccountingEntity extends AccountingDto {
    id: number;
}

export interface AccountingDto {
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