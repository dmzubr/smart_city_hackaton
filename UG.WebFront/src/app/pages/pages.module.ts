import { NgModule } from '@angular/core';
import { NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { PagesComponent } from './pages.component';
import { PagesRoutingModule } from './pages-routing.module';

// import { DashboardModule } from './dashboard/dashboard.module';
// import { UsersStatisticsModule } from './users-statistics/users-statistics.module';
// import { TriggersModule } from './triggers/triggers.module';
// import { WallPostsModule } from './wall-posts/wall-posts.module';
import { IndicatorsListModule } from './indicators-list/indicators-list.module';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,

    // DashboardModule,
    // UsersStatisticsModule,
    // TriggersModule,
    // WallPostsModule
    IndicatorsListModule
  ],
  declarations: [
    PagesComponent,
  ],
})
export class PagesModule {
}
