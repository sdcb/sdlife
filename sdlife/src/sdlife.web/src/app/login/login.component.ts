import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {

  }

  loginDto: LoginDto = {
    username: "",
    password: "",
    rememberMe: true
  };

  login() {
    console.log(this.loginDto);
  }
}

interface LoginDto {
  username: string;
  password: string;
  rememberMe: boolean;
}