import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-accounting',
  templateUrl: './accounting.component.html',
  styleUrls: ['./accounting.component.css']
})
export class AccountingComponent implements OnInit {

  constructor(
    private http: Http, 
    private router: Router) {
  }

  ngOnInit() {
    this.router.navigate(["/login"]);
  }

  test() {
    
  }

}
