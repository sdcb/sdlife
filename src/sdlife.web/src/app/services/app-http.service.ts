import { Injectable } from '@angular/core';
import { Http, XHRBackend, RequestOptionsArgs, Request, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/fromPromise'
import { TokenStorageService } from './token-storage.service';
import { Router } from '@angular/router';

@Injectable()
export class AppHttpService extends Http {
  constructor(
    backend: XHRBackend,
    options: RequestOptions,
    private tokenStorage: TokenStorageService,
    private router: Router) {
    super(backend, options);
  }

  onTokenInvalid() {
    return this.router.navigate(["login", {
      redirectUrl: "/"
    }]);
  }

  private requestByToken(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
    let token = this.tokenStorage.getToken();
    if (typeof url === "string") {
      options = options || {};
      options.headers = options.headers || new Headers();
      options.headers.append("Authorization", `bearer ${this.tokenStorage.getToken()}`);
    } else {
      url.headers.set("Authorization", `bearer ${this.tokenStorage.getToken()}`);
    }

    return super.request(url, options)
      .catch(this.onRequestFailed);
  }

  private onRequestFailed(err: Response) {
    if (err.status === 401 || err.status === 403) {
      this.onTokenInvalid();
    }
    return Observable.throw(err);
  }

  request(url: string | Request, options?: RequestOptionsArgs): Observable<any> {
    if (!this.tokenStorage.tokenExists() || this.tokenStorage.tokenExpired()) {
      return Observable.fromPromise(this.onTokenInvalid());
    }

    if (this.tokenStorage.tokenShouldRefresh()) {
      return super.request(new Request({
        method: "post",
        url: "/Account/RefreshToken",
        headers: new Headers({
          Authorization: "bearer " + this.tokenStorage.getToken()
        })
      }))
      .do(resp => {
        let json = resp.json();
        this.tokenStorage.store(
          json.token, 
          json.expiration, 
          json.refreshTime);
      })
      .map(x => this.requestByToken(url, options))
      .catch(this.onRequestFailed);
    } else {
      return this.requestByToken(url, options);
    }
  }
}
