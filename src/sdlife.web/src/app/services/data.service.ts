import { Injectable } from '@angular/core';
import { AppHttpService } from './app-http.service';

@Injectable()
export class DataService {
  constructor(private http: AppHttpService) { }

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

  private token() {
    return localStorage.getItem("token");
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