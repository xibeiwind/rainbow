import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DataFieldTypeComponent } from './data-field-type.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  { path: 'data-field-type', canActivate: [AuthGuard], component: DataFieldTypeComponent, data: { title: 'DataField' } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DataFieldTypeRoutingModule { }
