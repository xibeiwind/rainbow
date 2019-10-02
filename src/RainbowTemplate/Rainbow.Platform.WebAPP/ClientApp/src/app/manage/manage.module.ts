import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManageRoutingModule } from './manage-routing.module';
import { CoreModule } from '../core/core.module';
import { ManageComponent } from './manage/manage.component';

@NgModule({
  declarations: [
    ManageComponent,
  ],
  imports: [
    CommonModule,
    CoreModule,
    ManageRoutingModule
  ]
})
export class ManageModule { }
