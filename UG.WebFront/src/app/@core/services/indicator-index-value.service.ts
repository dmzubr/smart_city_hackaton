import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { ValuesContainerModel, IndicatorIndexValueModel } from '../model/';

@Injectable()
export class IndicatorIndexValueService extends BaseAPIService {

  constructor(
      http: HttpClient
  ) {
      super(http);
  }

  public SaveIndicatorIndexesChanges(changesList: IndicatorIndexValueModel[]): Observable<boolean> {
    let path = `${this.backendRootPath}/IndicatorIndexValue/SaveIndicatorIndexesChanges`;
    const passedData = JSON.stringify(changesList);
    return this.basePost<boolean>(path, passedData);
  }

  public SaveIndicatorIndexValue(valRec: IndicatorIndexValueModel) {
    let path = `${this.backendRootPath}/IndicatorIndexValue/ApplyIndicatorIndexValue`;
    const passedData = JSON.stringify(valRec);
    return this.basePost<boolean>(path, passedData);
  }
}
