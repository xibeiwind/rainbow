import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DataFieldTypeComponent } from './data-field-type.component';

const routes: Routes = [
  { path: 'data-field-type', component: DataFieldTypeComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DataFieldTypeRoutingModule { }
