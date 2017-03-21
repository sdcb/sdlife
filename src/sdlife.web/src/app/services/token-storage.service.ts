import { Injectable } from '@angular/core';

@Injectable()
export class TokenStorageService {

  constructor() { }

  store(token: string, expires: string, tokenRefreshTime: string) {
    localStorage.setItem("token", token);
    localStorage.setItem("token-expires", expires);
    localStorage.setItem("token-refresh", tokenRefreshTime);
  }

  getToken() {
    return (this.tokenExists() && !this.tokenExpired()) ?
      localStorage.getItem("token") :
      null;
  }

  tokenExists() {
    let token = localStorage.getItem("token");
    return token !== null;
  }

  tokenExpired() {
    let expires = localStorage.getItem("token-expires");
    return expires === null || (new Date(expires) < new Date());
  }

  tokenShouldRefresh() {
    let refresh = localStorage.getItem("token-refresh");
    return refresh === null || (new Date(refresh) < new Date());
  }

}
