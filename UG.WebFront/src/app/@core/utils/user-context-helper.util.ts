import { Injectable } from '@angular/core'
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import { AUTH_DATA } from '../services/auth/auth.data';

@Injectable()
export class UserContextHelper {

    private token: string;
    private companyId: number;
    private userName: string;

    jwtHelper: JwtHelperService = new JwtHelperService();

    public constructor(
        private router: Router
    ) {
        this.RefreshContext();
    }

    public RefreshContext(): void {
        const tokenVal = localStorage.getItem(AUTH_DATA.GetUserStorageKey());
        if (tokenVal) {
            const decodedToken = this.jwtHelper.decodeToken(tokenVal);
            if (decodedToken) {
                this.token = JSON.parse(tokenVal).token;
                this.companyId = decodedToken[AUTH_DATA.CLAIMS_KEYS.COMPANY_ID];
                this.userName = decodedToken[AUTH_DATA.CLAIMS_KEYS.USERNAME];
            }
        }

    }

    public GetAuthTokenVal(): string {
        return this.token;
    }

    public GetCurrentUserName(): string {
        return this.userName;
    }

  public GetCurrentCompanyId(): number {
    return this.companyId;
  }

    public IsInRole(roleName: string): boolean {
        let cUserStr = localStorage.getItem(AUTH_DATA.GetUserStorageKey());
        if (cUserStr) {
            let cUserObj = JSON.parse(cUserStr);
            const decodedToken = this.jwtHelper.decodeToken(cUserObj.token);
            let rolesList: string = decodedToken[AUTH_DATA.CLAIMS_KEYS.ROLES];
            let res = rolesList.indexOf(roleName) > -1;
            return res;
        }

        return false;
    }
}
