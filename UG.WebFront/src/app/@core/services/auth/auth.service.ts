import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { AUTH_DATA } from './auth.data';
import { BaseAPIService } from '../../services/base-api.service';
import { LoginResponseModel } from './login-response.model';

@Injectable()
export class AuthService extends BaseAPIService {

  public token: string;
  private authPath: string;

  constructor(
      http: HttpClient
  ) {

    super(http);

    this.authPath = `${this.backendRootPath}/Account/GenerateToken`;
    var currentUser = JSON.parse(localStorage.getItem(AUTH_DATA.GetUserStorageKey()));
    this.token = currentUser && currentUser.token;
  }

  public login(login: string, password: string): Observable<boolean> {
      let passedData = JSON.stringify({ login: login, password: password });

      return this.http.post(this.authPath, passedData, this.httpOptions).pipe(
      map((response: LoginResponseModel ) => {
          // login successful if there's a jwt token in the response
          let token = response.token;
          if (token) {
              // set token property
              this.token = token;

              // storage username and jwt token in local, storage to keep user logged in between page refreshes
              localStorage.setItem(AUTH_DATA.GetUserStorageKey(), JSON.stringify({ username: login, token: token }));

              // return true to indicate successful login
              return true;
          } else {
              // return false to indicate failed login
              return false;
          }
      }),
      catchError(this.handleError));
  }

  public logout(): void {
      // clear token remove user from local storage to log user out
      this.token = null;
      localStorage.removeItem(AUTH_DATA.GetUserStorageKey());
  }

  public register(email: string, password: string, phone: string, terms: boolean): Observable<boolean> {
    let passedData = JSON.stringify({
      login: email,
      password: password,
      terms: terms,
      phone: phone
    });

    const targetUrl = `${this.backendRootPath}/Account/Register`;
    return this.basePost<boolean>(targetUrl, passedData);
  }
}
