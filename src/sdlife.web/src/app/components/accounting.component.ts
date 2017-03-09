import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService, AccountingDto } from '../services/data.service';
import { Observable } from 'rxjs/Rx';

@Component({
  selector: 'app-accounting',
  templateUrl: './accounting.component.html',
  styleUrls: ['./accounting.component.css'], 
  providers: [DataService]
})
export class AccountingComponent implements OnInit {
  private accountings: any;

  constructor(
    private router: Router, 
    private data: DataService) {
  }

  ngOnInit() {
    if (localStorage.getItem("token") === null) {
      this.router.navigate(["/login", {
        redirectUrl: "/"
      }]);
    }

    this.accountings = this.data.loadAccountingInRange("2017/1/1", "2017/2/1", null);
  }
}
