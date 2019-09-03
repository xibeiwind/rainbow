import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [

  { path: 'auth', loadChildren: './auth/auth.module#AuthModule', data: { title: '', customLayout: true } },
  { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule', data: { title: '控制面板', customLayout: false } },
  { path: 'admin', loadChildren: './admin/admin.module#AdminModule', data: { title: '管理', customLayout: false } },

  { path: '**', pathMatch: 'full', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
