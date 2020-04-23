import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { LayoutModule } from 'angular-admin-lte';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrModule } from 'ngx-toastr';
import { UiSwitchModule } from 'ngx-ui-switch';
import { adminLteConf } from './admin-lte.conf';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard } from './auth.guard';
import { CoreModule } from './core/core.module';
import { ModelModule } from './model/model.module';
import { ServiceModule } from './services/service.module';
import { SysAdminAuthGuard } from './sys-admin-auth.guard';
import { toastrConfig } from './toastrConfig';



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
