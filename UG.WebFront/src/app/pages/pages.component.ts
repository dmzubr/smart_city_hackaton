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
      ];
      const managerRoutesList = [
        '/pages/complaints',
        '/pages/complaints/-1',
        '/pages/manage-attributes'
      ];
      const botManagerRoutesList = [
        '/pages/bot-forms',
      ];
      const citizenRoutesList = [
      ];

      for(let item of start_menu) {
        if (commonRoutesList.indexOf(item.link) > -1 || item.group || item.children)
          resList.push(item);

        if (this.userHelper.IsInRole('manager') && managerRoutesList.indexOf(item.link) > -1)
          resList.push(item);

        if (this.userHelper.IsInRole('citizen') && citizenRoutesList.indexOf(item.link) > -1)
          resList.push(item);

        if (this.userHelper.IsInRole('bot_company_manager') && botManagerRoutesList.indexOf(item.link) > -1)
          resList.push(item);
      }
    }

    this.menu = resList;
  }
}
