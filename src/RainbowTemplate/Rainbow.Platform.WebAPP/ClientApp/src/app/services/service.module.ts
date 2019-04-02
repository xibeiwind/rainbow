import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SiteService } from './site.service';
import { AccountService } from './AccountService';
import { MessageService } from './MessageService';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    SiteService,
    AccountService,
    MessageService,
  ],
})
export class ServiceModule { }
