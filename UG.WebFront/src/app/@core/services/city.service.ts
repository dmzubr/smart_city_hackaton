import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseAPIService } from './base-api.service';
import { CityModel } from '../model/';

@Injectable()
export class CityService extends BaseAPIService {

    constructor(
        http: HttpClient
    ) {
        super(http);
    }

    public GetList(): Observable<CityModel[]>  {
        let path = `${this.backendRootPath}/City/GetList`;
        return this.baseGet<CityModel[]>(path);
    }
}
