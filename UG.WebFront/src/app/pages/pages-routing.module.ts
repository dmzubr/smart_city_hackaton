import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { IndicatorsListComponent } from './indicators-list/indicators-list.component';
import { OuterDataSourcesComponent } from './outer-data-sources/outer-data-sources.component';
import { OuterMetricValuesComponent } from './outer-metric-values/outer-metric-values.component';

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
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
