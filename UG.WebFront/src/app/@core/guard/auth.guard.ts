import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import { AUTH_DATA } from '../services/auth/auth.data';

@Injectable()
export class AuthGuard implements CanActivate {

    jwtHelper: JwtHelperService = new JwtHelperService();

    constructor(
        private router: Router
    ) { }

    canActivate() {

        var cUserStr = localStorage.getItem(AUTH_DATA.GetUserStorageKey());
        if (cUserStr) {
            var cUserObj = JSON.parse(cUserStr);
            if (!this.jwtHelper.isTokenExpired(cUserObj.token)) {
                // logged in and token is not expired - so return true
                return true;
            }
        }

        // not logged  in so redirect to login page
        console.debug('Redirect to login from AuthGuard')
        this.router.navigate(['/auth/login']);
        return false;
    }
}
