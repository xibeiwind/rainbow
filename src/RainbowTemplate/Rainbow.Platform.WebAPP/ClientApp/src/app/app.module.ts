import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule, } from 'angular-admin-lte';
import { ModalModule, BsDropdownModule, PaginationModule } from 'ngx-bootstrap';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { ToastrModule } from 'ngx-toastr';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { UiSwitchModule } from 'ngx-ui-switch';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { adminLteConf } from './admin-lte.conf';
import { ServiceModule } from './services/service.module';
import { ModelModule } from './model/model.module';
import { AuthGuard } from './auth.guard';
import { toastrConfig } from './toastrConfig';
import { SysAdminAuthGuard } from './sys-admin-auth.guard';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    LayoutModule.forRoot(adminLteConf),
    LoadingPageModule,
    MaterialBarModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot(toastrConfig),
    BsDropdownModule.forRoot(),
    SweetAlert2Module.forRoot(),
    PaginationModule.forRoot(),
    UiSwitchModule.forRoot({
      size: 'small',
      color: 'rgb(0, 189, 99)',
      // switchColor: '#80FFA2',
      switchColor: '#fff',
      defaultBgColor: 'rgb(180,180,180)',
      defaultBoColor: 'rgb(180,180,180)',
      checkedLabel: '是',
      uncheckedLabel: '否'
    }),
    ServiceModule,
    ModelModule,
    AppRoutingModule,
  ],
  providers: [AuthGuard, SysAdminAuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
