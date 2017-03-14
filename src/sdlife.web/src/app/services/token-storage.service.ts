import { Injectable } from '@angular/core';

@Injectable()
export class TokenStorageService {

  constructor() { }

  store(token: string, expires: string) {
    localStorage.setItem("token", token);
    localStorage.setItem("token-expires", expires);
  }

  getToken() {
    return this.isTokenValid() ?
      localStorage.getItem("token") :
      null;
  }

  isTokenValid() {
    let token = localStorage.getItem("token");
    if (token === null) return false;

    let expires = localStorage.getItem("token-expires");
    return new Date(expires) > new Date();
  }

}
