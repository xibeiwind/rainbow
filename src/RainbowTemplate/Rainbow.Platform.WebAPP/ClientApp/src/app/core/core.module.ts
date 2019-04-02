import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderInnerComponent } from './header-inner/header-inner.component';
import { SidebarLeftInnerComponent } from './sidebar-left-inner/sidebar-left-inner.component';
import { SidebarRightInnerComponent } from './sidebar-right-inner/sidebar-right-inner.component';
import { ConfirmModalComponent } from './confirm-modal/confirm-modal.component';
import { EditModalComponent } from './edit-modal/edit-modal.component';
import { DropdownModule, TabsModule } from 'angular-admin-lte';

@NgModule({
  declarations: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent
  ],
  imports: [
    CommonModule,
    DropdownModule,
    TabsModule,
  ],
  exports: [
    HeaderInnerComponent,
    SidebarLeftInnerComponent,
    SidebarRightInnerComponent,
    ConfirmModalComponent,
    EditModalComponent

  ]

})
export class CoreModule { }
