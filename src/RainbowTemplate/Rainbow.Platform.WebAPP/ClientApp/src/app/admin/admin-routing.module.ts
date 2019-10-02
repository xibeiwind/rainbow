import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { RoleInfoComponent } from './role-info/role-info.component';
import { ClientModuleComponent } from './client-module/client-module.component';
import { ControllerProjectComponent } from './controller-project/controller-project.component';
import { AuthGuard } from '../auth.guard';
import { SysAdminAuthGuard } from '../sys-admin-auth.guard';

const routes: Routes = [
  {
    path: '', component: AdminComponent,
    canActivate: [SysAdminAuthGuard],
    canLoad: [SysAdminAuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      { path: 'role', component: RoleInfoComponent, data: { title: '用户角色管理' } },
      { path: 'client-module', component: ClientModuleComponent, data: { title: 'Client Module管理' } },
      { path: 'controller-project', component: ControllerProjectComponent, data: { title: 'Controller 项目管理' } },
      { path: '', pathMatch: 'full', redirectTo: 'role' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
