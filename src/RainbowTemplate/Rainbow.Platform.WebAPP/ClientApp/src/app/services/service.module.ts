﻿
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SiteService } from './site.service';

import { AccountService } from './AccountService';
import { CustomerServiceAccountService } from './CustomerServiceAccountService';
import { DataFieldTypeService } from './DataFieldTypeService';
import { EnumDisplayService } from './EnumDisplayService';
import { LookupQueryService } from './LookupQueryService';
import { MessageService } from './MessageService';
import { ModelService } from './ModelService';
import { RoleInfoService } from './RoleInfoService';
import { UserService } from './UserService';
import { ViewModelDisplayService } from './ViewModelDisplayService';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    SiteService,
    AccountService,
    CustomerServiceAccountService,
    DataFieldTypeService,
    EnumDisplayService,
    LookupQueryService,
    MessageService,
    ModelService,
    RoleInfoService,
    UserService,
    ViewModelDisplayService,
  ],
})
export class ServiceModule { }
