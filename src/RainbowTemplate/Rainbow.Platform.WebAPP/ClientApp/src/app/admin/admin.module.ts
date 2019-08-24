import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { CoreModule } from '../core/core.module';
import { AdminComponent } from './admin/admin.component';
import { RoleInfoComponent } from './role-info/role-info.component';
import { DataFieldTypeComponent } from './data-field-type/data-field-type.component';

@NgModule({
  declarations: [AdminComponent, RoleInfoComponent, DataFieldTypeComponent],
  imports: [
    CommonModule,
    CoreModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
