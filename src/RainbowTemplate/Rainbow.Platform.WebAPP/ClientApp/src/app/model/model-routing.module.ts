import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ModelComponent } from './model.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  {
    path: 'model',
    canActivate: [AuthGuard],
    component: ModelComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ModelRoutingModule { }
