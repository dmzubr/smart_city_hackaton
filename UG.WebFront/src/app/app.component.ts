/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AnalyticsService } from './@core/utils/analytics.service';
import { AuthService } from './@core/services/auth/auth.service';
import { NbMenuService } from '@nebular/theme';

@Component({
  selector: 'ngx-app',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {

  constructor(
    private analytics: AnalyticsService,
    private menuService: NbMenuService,
    private authService: AuthService,
    private router: Router) {
  }

  ngOnInit() {
    this.analytics.trackPageViews();
    this.menuService.onItemClick()
      .subscribe((event) => {
        this.onContextItemSelection(event.item);
      });
  }

  onContextItemSelection(menuItem) {
    if (menuItem.isLogout)
      this.authService.logout();

    if (menuItem.route)
      this.router.navigate([menuItem.route]);
  }
}
