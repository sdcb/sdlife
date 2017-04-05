import { Injectable } from '@angular/core';


const Token = "token";
const TokenExpires = "token-expires";
const TokenRefresh = "token-refresh";

@Injectable()
export class TokenStorageService {

  constructor() { }

  store(token: string, expires: string, tokenRefreshTime: string) {
    localStorage.setItem(Token, token);
    localStorage.setItem(TokenExpires, expires);
    localStorage.setItem(TokenRefresh, tokenRefreshTime);
  }

  getToken() {
    return (this.tokenExists() && !this.tokenExpired()) ?
      localStorage.getItem(Token) :
      null;
  }

  tokenExists() {
    let token = localStorage.getItem(Token);
    return token !== null;
  }

  tokenExpired() {
    let expires = localStorage.getItem(TokenExpires);
    return expires === null || (new Date(expires) < new Date());
  }

  tokenShouldRefresh() {
    let refresh = localStorage.getItem(TokenRefresh);
    return refresh === null || (new Date(refresh) < new Date());
  }

  removeToken() {
    localStorage.removeItem(Token);
    localStorage.removeItem(TokenExpires);
    localStorage.removeItem(TokenRefresh);
  }

}
