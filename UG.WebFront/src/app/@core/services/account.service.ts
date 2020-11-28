import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
// import { AccountModel } from '../model/';

@Injectable()
export class AccountService extends BaseAPIService{

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    /*
    public GetAccountInfo(): Observable<AccountModel>  {
        let path = `${this.backendRootPath}/Account/GetAccountInfo`;
        return this.baseGet<AccountModel>(path);
    }

    public CreateAddBalanceRequest(): Observable<boolean>  {
      let path = `${this.backendRootPath}/MoneyTransaction/CreateAddBalanceRequest`;
      return this.basePost<boolean>(path);
    }
    */
}
