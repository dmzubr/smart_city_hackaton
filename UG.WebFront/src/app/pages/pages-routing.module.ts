import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { IndicatorsListComponent } from './indicators-list/indicators-list.component';
// import { ComplaintDetailedComponent } from './complaint-detailed/complaint-detailed.component';
// import { ManageAttributesComponent } from './manage-attributes/manage-attributes.component';
//
// import { BotManagerComponent } from './bot-manager/bot-manager.component';
// import { BotFormsComponent } from './bot-forms/bot-forms.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [
    {
      path: 'indicators-list',
      component: IndicatorsListComponent,
    },
    // {
    //   path: 'bot-forms',
    //   component: BotFormsComponent,
    // },
    // {
    //   path: 'manage-attributes',
    //   component: ManageAttributesComponent,
    // },
    // {
    //   path: 'complaints',
    //   component: ComplaintsComponent,
    // },
    // {
    //   path: '',
    //   redirectTo: 'complaints',
    //   pathMatch: 'full',
    // },
    // {
    //   path: 'complaints/:complaint-id',
    //   component: ComplaintDetailedComponent
    // }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
