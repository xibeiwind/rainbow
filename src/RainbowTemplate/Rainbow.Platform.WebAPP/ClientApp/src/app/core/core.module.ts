import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CKEditorModule } from 'ckeditor4-angular';
import { PaginationModule } from 'ngx-bootstrap';
import { HeaderInnerComponent } from './header-inner/header-inner.component';
import { SidebarLeftInnerComponent } from './sidebar-left-inner/sidebar-left-inner.component';
import { SidebarRightInnerComponent } from './sidebar-right-inner/sidebar-right-inner.component';
import { ConfirmModalComponent } from './confirm-modal/confirm-modal.component';
import { EditModalComponent } from './edit-modal/edit-modal.component';
import { DropdownModule, TabsModule } from 'angular-admin-lte';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DataListComponent } from './data-list/data-list.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { RouterModule } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { PagingDataListComponent } from './paging-data-list/paging-data-list.component';

@NgModule({
  declarations: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent,
    CreateModalComponent,
    DataListComponent,
    SearchComponent,
    PagingDataListComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    TabsModule,
    SweetAlert2Module,
    CKEditorModule,
    PaginationModule,
  ],
  exports: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent,
    CreateModalComponent,
    DataListComponent,
    PagingDataListComponent,
  ]

})
export class CoreModule { }
