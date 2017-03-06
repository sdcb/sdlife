import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-accounting',
  templateUrl: './accounting.component.html',
  styleUrls: ['./accounting.component.css']
})
export class AccountingComponent implements OnInit {
  constructor(
    private router: Router) {
  }

  ngOnInit() {
    if (localStorage.getItem("token") === null) {
      this.router.navigate(["/login", {
        redirectUrl: "/"
      }]);
    }
  }
}
