import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { SubindexViewModel } from '../model/';

@Injectable()
export class SubindexService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetVMList(): Observable<SubindexViewModel[]>  {
        let path = `${this.backendRootPath}/SubIndex/GetVMList`;
        return this.baseGet<SubindexViewModel[]>(path);
    }
}
