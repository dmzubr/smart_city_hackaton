import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../../environments/environment';

@Injectable()
export class BaseAPIService {

    protected backendRootPath = environment.backendRoot;

    protected httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    constructor(
        protected http: HttpClient
    ) { }

    protected baseGet<T>(path: string): Observable<T> {
        return this.http.get(path).pipe(
            map((res: T) =>  {
                return res;
            }),
            catchError(this.handleError));
    }

    protected basePost<T>(path: string, passedData?: string): Observable<T> {
        return this.http.post(path, passedData, this.httpOptions).pipe(
            map((res: T) =>  {
                return res;
            }),
            catchError(this.handleError));
    }

    protected baseDelete<T>(path: string): Observable<T> {
        return this.http.delete(path).pipe(
            map((res: T) =>  {
                return res;
            }),
            catchError(this.handleError));
    }

    protected handleError (error: HttpErrorResponse | any) {
        let errMsg: string;
        if (error instanceof HttpErrorResponse) {
            if (error.status === 0) {
                errMsg = 'Отсутстует подключение к интернету, либо произошла на стороне сервера!';
            } else {
                if (error.status == 404) {
                    errMsg = 'Запрошенный адрес не найден!';
                } else if (error.status == 401) {
                  errMsg = 'Недостаточно прав для получения данных!';
                } else {
                    errMsg = typeof error.error === 'string' ? error.error : error.error.value || JSON.stringify(error.error);
                }
            }
        } else {
            errMsg = error.message ? error.messsage : error.toString();
        }
        return throwError(errMsg);
    }
}
