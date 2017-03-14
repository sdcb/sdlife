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

  request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
    if (!this.tokenStorage.isTokenValid()) {
      Observable.fromPromise(this.onTokenInvalid());
    }

    let token = this.tokenStorage.getToken();
    if (typeof url === "string") {
      options = options || {};
      options.headers = options.headers || new Headers();
      options.headers.append("Authorization", `bearer ${this.tokenStorage.getToken()}`);
    } else {
      url.headers.set("Authorization", `bearer ${this.tokenStorage.getToken()}`);
    }

    return super.request(url, options)
      .catch((err: Response, caught) => {
        if (err.status === 401 || err.status === 403) {
          this.onTokenInvalid();
        }
        return Observable.throw(err);
      });
  }
}
