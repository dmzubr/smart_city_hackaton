import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AUTH_DATA } from '../services/auth/auth.data';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(
    ) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      let injectedHeaderVal = ''
      const cUserStr = localStorage.getItem(AUTH_DATA.GetUserStorageKey());
      if (cUserStr) {
        let cUserObj = JSON.parse(cUserStr);
        injectedHeaderVal = `Bearer ${cUserObj.token}`
      }

      if (injectedHeaderVal.length > 0)
          request = request.clone({
              setHeaders: {
                  'Authorization': injectedHeaderVal
              }
          });

      return next.handle(request);
    }
}
