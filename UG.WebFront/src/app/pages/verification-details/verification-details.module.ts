import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import {
  NbAccordionModule,
  NbCardModule,
  NbListModule,
  NbIconModule,
  NbProgressBarModule,
  NbCheckboxModule,
  NbSelectModule
} from "@nebular/theme";
import { Ng2SmartTableModule } from 'ng2-smart-table';

import { ThemeModule } from '../../@theme/theme.module';
import { VerificationDetailsComponent } from './verification-details.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,

    NbAccordionModule,
    NbCardModule,
    NbListModule,
    NbIconModule,
    NbProgressBarModule,
    Ng2SmartTableModule,
    NbCheckboxModule,
    NbSelectModule,

    ThemeModule,
  ],
  declarations: [
    VerificationDetailsComponent
  ]
})
export class VerificationDetailsModule { }
