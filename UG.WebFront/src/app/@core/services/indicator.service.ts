import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { IndicatorModel } from '../model/';

@Injectable()
export class IndicatorService extends BaseAPIService {

  constructor(
      http: HttpClient
  ) {
      super(http);
  }

  public Get(indicatorId: number): Observable<IndicatorModel> {
    let path = `${this.backendRootPath}/Indicator/Get?indicatorId=${indicatorId}`;
    return this.baseGet<IndicatorModel>(path);
  }

  public GetEmptyObj(): IndicatorModel {
    return new IndicatorModel(0,0,'',0);
  }
}
