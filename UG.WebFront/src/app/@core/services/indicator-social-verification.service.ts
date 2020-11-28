import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { IndicatorSocialVerificationModel } from '../model/';

@Injectable()
export class IndicatorSocialVerificationService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetList(year: number): Observable<IndicatorSocialVerificationModel[]>  {
        let path = `${this.backendRootPath}/IndicatorSocialVerification/GetValues?year=${year}`;
        return this.baseGet<IndicatorSocialVerificationModel[]>(path);
    }
}
