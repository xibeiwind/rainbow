import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule, } from 'angular-admin-lte';
import { ModalModule, } from 'ngx-bootstrap';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { adminLteConf } from './admin-lte.conf';
import { ServiceModule } from './services/service.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    LayoutModule.forRoot(adminLteConf),
    ModalModule.forRoot(),
    LoadingPageModule,
    MaterialBarModule,
    ServiceModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
