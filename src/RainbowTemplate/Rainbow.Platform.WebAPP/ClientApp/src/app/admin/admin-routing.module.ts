import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { RoleInfoComponent } from './role-info/role-info.component';
import { DataFieldTypeComponent } from './data-field-type/data-field-type.component';
import { ClientModuleComponent } from './client-module/client-module.component';

const routes: Routes = [
  {
    path: '', component: AdminComponent,
    children: [
      { path: 'role', component: RoleInfoComponent, data: { title: '用户角色管理' } },
      { path: 'data-field-type', component: DataFieldTypeComponent, data: { title: 'DataFieldType' } },
      { path: 'client-module', component: ClientModuleComponent, data: { title: 'Client Module' } },
      { path: '', pathMatch: 'full', redirectTo: 'role' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
