import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DataFieldTypeRoutingModule } from './data-field-type-routing.module';
import { DataFieldTypeComponent } from './data-field-type.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [DataFieldTypeComponent],
  imports: [
    CommonModule,
    CoreModule,
    DataFieldTypeRoutingModule
  ]
})
export class DataFieldTypeModule { }
