import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BoxModule } from 'angular-admin-lte';

import { ModelRoutingModule } from './model-routing.module';
import { ModelComponent } from './model.component';
import { UiSwitchModule } from 'ngx-ui-switch';

@NgModule({
  declarations: [ModelComponent],
  imports: [
    CommonModule,
    FormsModule,
    BoxModule,
    UiSwitchModule,
    ModelRoutingModule
  ]
})
export class ModelModule { }
