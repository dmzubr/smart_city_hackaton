import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { ValuesContainerModel } from '../model/';

@Injectable()
export class IndicatorValueService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetValuesContainer(): Observable<ValuesContainerModel>  {
        let path = `${this.backendRootPath}/IndicatorValue/GetValuesContainer`;
        return this.baseGet<ValuesContainerModel>(path);
    }

    public GetEmptyContainer(): ValuesContainerModel {
      return new ValuesContainerModel([], []);
    }
}
