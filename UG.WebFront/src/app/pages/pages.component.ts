import { Component } from '@angular/core';

import { MENU_ITEMS } from './pages-menu';
import { NbMenuItem } from "@nebular/theme";
import { UserContextHelper } from '../@core/utils/user-context-helper.util';

@Component({
  selector: 'ngx-pages',
  styleUrls: ['pages.component.scss'],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class PagesComponent {

  menu: NbMenuItem[];

  constructor(
    private userHelper: UserContextHelper
  ){
    let start_menu = MENU_ITEMS;
    let resList = []
    if (this.userHelper.IsInRole('admin')) {
      resList = MENU_ITEMS;
    }
    else  {
      const commonRoutesList = [
        '/pages/indicators-list',
      ];
      const regionalManagerRoutesList = [

      ];
      const centralManagerRoutesList = [
        '/pages/outer-data-sources'
      ];

      for(let item of start_menu) {
        if (commonRoutesList.indexOf(item.link) > -1 || item.group || item.children)
          resList.push(item);

        if (this.userHelper.IsInRole('region_manager') && regionalManagerRoutesList .indexOf(item.link) > -1)
          resList.push(item);

        if (this.userHelper.IsInRole('central_manager') && centralManagerRoutesList.indexOf(item.link) > -1)
          resList.push(item);
      }
    }

    this.menu = resList;
  }
}
