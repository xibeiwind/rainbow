import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { BoxSmallModule, BoxModule, BoxInfoModule } from 'angular-admin-lte';


@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    BoxModule,
    BoxInfoModule,
    BoxSmallModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
