import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { OuterDataSourceModelViewModel } from '../model/';

@Injectable()
export class OuterDataSourceService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetVMList(): Observable<OuterDataSourceModelViewModel[]>  {
        let path = `${this.backendRootPath}/OuterMetricDataSource/GetVMList`;
        return this.baseGet<OuterDataSourceModelViewModel[]>(path);
    }
}
