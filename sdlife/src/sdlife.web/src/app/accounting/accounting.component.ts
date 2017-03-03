import { Component, OnInit } from '@angular/core';
import { Http, HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-accounting',
  templateUrl: './accounting.component.html',
  styleUrls: ['./accounting.component.css']
})
@NgModule({
  imports: [
    HttpModule
  ]
})
export class AccountingComponent implements OnInit {

  constructor(private http: Http) {

  }

  ngOnInit() {
  }

  test() {
    this.http.post("/Accounting/List", {
      title: "早餐"
    }).subscribe(resp => {
      console.log(resp.json());
    });
  }

}
