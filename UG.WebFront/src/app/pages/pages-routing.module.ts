import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { IndicatorsListComponent } from './indicators-list/indicators-list.component';
import { OuterDataSourcesComponent } from './outer-data-sources/outer-data-sources.component';
import { OuterMetricValuesComponent } from './outer-metric-values/outer-metric-values.component';
import { VerificationDetailsComponent } from './verification-details/verification-details.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [
    {
      path: 'indicators-list',
      component: IndicatorsListComponent
    },
    {
      path: 'outer-data-sources',
      component: OuterDataSourcesComponent
    },
    {
      path: 'outer-metric-values/:metric-id',
      component: OuterMetricValuesComponent
    },
    {
      path: 'verify-indicator/:indicator-id',
      component: VerificationDetailsComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
