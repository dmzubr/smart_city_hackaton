// @ts-ignore

import { Component, ChangeDetectorRef, Inject, ViewEncapsulation} from '@angular/core';
import { Router } from '@angular/router';

import { NB_AUTH_OPTIONS, NbLoginComponent } from '@nebular/auth';
import { AuthService } from '../../@core/services/auth/auth.service';
// import { NbAuthService } from '@nebular/auth/services/auth.service';

export class LoginModel {
  public constructor(
    public email: string,
    public password: string
  ) {}
}

@Component({
  selector: 'auth-login',
  styleUrls: ['./login.component.scss'],
  templateUrl: './login.component.html',
  providers: [
    AuthService
  ]
  // encapsulation: ViewEncapsulation.ShadowDom
})
export class LoginComponent extends NbLoginComponent {

  messages: string[] = [];
  errors: string[] = [];

  public user: LoginModel = new LoginModel('','');

  public constructor(
    // protected service: NbAuthService,
    @Inject(NB_AUTH_OPTIONS) protected options = {},
    protected cd: ChangeDetectorRef,
    protected router: Router,
    private authService: AuthService) {
    super(null, options, cd, router);
    this.rememberMe = false;
  }

  loading: boolean = false;
  submitted: boolean = false;
  public submitForm() {
    this.submitted = true;
    this.loading = true;
    this.authService.login(this.user.email, this.user.password).subscribe(res => {
      this.loading = false;
      if (res) {
        this.messages.push('Успешный вход.');
        this.router.navigate(['/pages/users-statistics']);
      }
      else
        this.errors.push('Что-то пошло не так! Пожалуйста обратитесь в службу поддержки.');
    },
    (error: any) => {
      this.loading = false;
      this.errors.push(error);
    });
  }
}
