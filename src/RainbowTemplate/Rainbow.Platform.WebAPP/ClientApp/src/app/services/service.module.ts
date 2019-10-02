import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SiteService } from './site.service';
import { InputTypeService } from './InputTypeService';
import { EnumCacheService } from './EnumCacheService';

import { AccountService } from './AccountService';
import { ClientModuleService } from './ClientModuleService';
import { ControllerProjectService } from './ControllerProjectService';
import { EnumDisplayService } from './EnumDisplayService';
import { LookupQueryService } from './LookupQueryService';
import { MessageService } from './MessageService';
import { ModelService } from './ModelService';
import { RoleInfoService } from './RoleInfoService';
import { ServiceManagementService } from './ServiceManagementService';
import { TypeQueryService } from './TypeQueryService';
import { UserService } from './UserService';
import { ViewModelDisplayService } from './ViewModelDisplayService';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    SiteService,
    InputTypeService,
    EnumCacheService,
    AccountService,
    ClientModuleService,
    ControllerProjectService,
    EnumDisplayService,
    LookupQueryService,
    MessageService,
    ModelService,
    RoleInfoService,
    ServiceManagementService,
    TypeQueryService,
    UserService,
    ViewModelDisplayService,
  ],
})
export class ServiceModule { }
