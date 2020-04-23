import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { DropdownModule, TabsModule } from 'angular-admin-lte';
import { AqmModule } from "angular-qq-maps";
import { CKEditorModule } from 'ckeditor4-angular';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { UiSwitchModule } from 'ngx-ui-switch';
import { CheckboxListComponent } from './checkbox-list/checkbox-list.component';
import { ConfirmModalComponent } from './confirm-modal/confirm-modal.component';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { DataListComponent } from './data-list/data-list.component';
import { EditFormComponent } from './edit-form/edit-form.component';
import { EditModalComponent } from './edit-modal/edit-modal.component';
import { HeaderInnerComponent } from './header-inner/header-inner.component';
import { ImgPreviewableComponent } from './img-previewable/img-previewable.component';
import { ItemDetailComponent } from './item-detail/item-detail.component';
import { LocationDisplayComponent } from './location-display/location-display.component';
import { LocationSelectComponent } from './location-select/location-select.component';
import { LookupDisplayComponent } from './lookup-display/lookup-display.component';
import { LookupSelectComponent } from './lookup-select/lookup-select.component';
import { PagingDataListComponent } from './paging-data-list/paging-data-list.component';
import { SearchComponent } from './search/search.component';
import { SelectImageFileComponent } from './select-image-file/select-image-file.component';
import { SidebarLeftInnerComponent } from './sidebar-left-inner/sidebar-left-inner.component';
import { SidebarRightInnerComponent } from './sidebar-right-inner/sidebar-right-inner.component';

@NgModule({
  declarations: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent,
    CreateModalComponent,
    SearchComponent,
    PagingDataListComponent,
    DataListComponent,
    CheckboxListComponent,
    ItemDetailComponent,
    SelectImageFileComponent,
    LookupSelectComponent,
    LookupDisplayComponent,
    ImgPreviewableComponent,
    LocationSelectComponent,
    EditFormComponent,
    LocationDisplayComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    TypeaheadModule,
    TabsModule,
    SweetAlert2Module,
    CKEditorModule,
    PaginationModule,
    UiSwitchModule,
    AqmModule,
  ],
  exports: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent,
    CreateModalComponent,
    PagingDataListComponent,
    DataListComponent,
    CheckboxListComponent,
    ItemDetailComponent,
    SelectImageFileComponent,
    LookupSelectComponent,
    LookupDisplayComponent,
    EditFormComponent,
    LocationDisplayComponent,
  ]

})
export class CoreModule { }
