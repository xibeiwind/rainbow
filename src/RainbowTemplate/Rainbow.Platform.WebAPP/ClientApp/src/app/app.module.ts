import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule, } from 'angular-admin-lte';
import { ModalModule, BsDropdownModule } from 'ngx-bootstrap';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';
import { ToastrModule } from 'ngx-toastr';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { adminLteConf } from './admin-lte.conf';
import { ServiceModule } from './services/service.module';
import { ModelModule } from './model/model.module';
import { DataFieldTypeModule } from './data-field-type/data-field-type.module';
import { AuthGuard } from './auth.guard';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    LayoutModule.forRoot(adminLteConf),
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
    BsDropdownModule.forRoot(),
    SweetAlert2Module.forRoot(),
    LoadingPageModule,
    MaterialBarModule,
    ServiceModule,
    ModelModule,
    DataFieldTypeModule,
    AppRoutingModule,
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
