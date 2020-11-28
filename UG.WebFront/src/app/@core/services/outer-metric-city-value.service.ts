import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { OuterMetricValueViewModel } from '../model/';

@Injectable()
export class OuterMetricCityValueService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetValuesByMetric(outerMetricId: number): Observable<OuterMetricValueViewModel[]>  {
        let path = `${this.backendRootPath}/OuterMetricValue/GetValuesByMetric?outerMetricId=${outerMetricId}`;
        return this.baseGet<OuterMetricValueViewModel[]>(path);
    }
}
