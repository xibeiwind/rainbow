// tslint:disable:no-empty-interface

declare namespace Rainbow.Common.Enums {
}
declare namespace Rainbow.ViewModels {
  interface DisplayQueryVM {
    Name?: string;
  }
  interface EnumDisplayVM {
    DisplayName?: string;
    Fields: Rainbow.ViewModels.EnumFieldDisplayVM[];
    FullName?: string;
    Name?: string;
  }
  interface EnumFieldDisplayVM {
    DisplayName?: string;
    Name?: string;
    Value: number;
  }
  interface FieldDisplayVM {
    DataType: System.ComponentModel.DataAnnotations.DataType;
    DisplayName?: string;
    FieldType?: string;
    IsEnum: boolean;
    IsNullable: boolean;
    Lookup?: Rainbow.ViewModels.LookupSettingVM;
    Name?: string;
  }
  interface LookupQueryVM {
    FieldName?: string;
    Filter?: string;
    VMName?: string;
  }
  interface LookupResultVM extends Yunyong.Core.ViewModels.VMBase {
    Name?: any;
    Value?: any;
  }
  interface LookupSettingVM {
    DisplayField?: string;
    ValueField?: string;
    VMType?: string;
  }
  interface ModelDisplaySuitVM {
    DisplayName?: string;
    ModelName?: string;
    ViewModels?: Rainbow.ViewModels.ViewModelDisplayVM[];
  }
  interface ViewModelDisplayVM {
    DisplayName?: string;
    Fields?: Rainbow.ViewModels.FieldDisplayVM[];
    ModelName?: string;
    Name?: string;
    Type: Rainbow.Common.Enums.VMType;
  }
}
declare namespace Rainbow.ViewModels.DataFieldTypes {
  interface CreateDataFieldTypeVM extends Yunyong.Core.ViewModels.CreateVM {
    FieldTypeDisplay?: string;
    FieldTypeEdit?: string;
    Type: System.ComponentModel.DataAnnotations.DataType;
  }
  interface DataFieldTypeVM extends Yunyong.Core.ViewModels.VMBase {
    FieldTypeDisplay?: string;
    FieldTypeEdit?: string;
    Type: System.ComponentModel.DataAnnotations.DataType;
  }
  interface DeleteDataFieldTypeVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface QueryDataFieldTypeVM extends Yunyong.Core.PagingQueryOption {
    FieldTypeDisplay?: string;
    FieldTypeEdit?: string;
    Type?: System.ComponentModel.DataAnnotations.DataType;
  }
  interface UpdateDataFieldTypeVM extends Yunyong.Core.ViewModels.UpdateVM {
    FieldTypeDisplay?: string;
    FieldTypeEdit?: string;
  }
}
declare namespace Rainbow.ViewModels.Messages {
  interface MessageQueryOption extends Yunyong.Core.PagingQueryOption {
  }
  interface MessageVM extends Yunyong.Core.ViewModels.VMBase {
    SendFrom?: string;
    SendFromId: string;
    SendOn: Date;
    SendTo?: string;
    SendToId: string;
    Summary?: string;
    Title?: string;
  }
}
declare namespace Rainbow.ViewModels.Models {
  interface CreateModelSuitApplyVM {
    EnableDelete: boolean;
    FolderName?: string;
    GenerateController: boolean;
    GenerateNgModuleComponent: boolean;
    GenerateService: boolean;
    GenerateVM: boolean;
    IsNgModelListComponent: boolean;
    Items?: Rainbow.ViewModels.Models.CreateViewModelApplyVM[];
    ModelFullName?: string;
    ModelName?: string;
    NgModuleName?: string;
    UpdateTsServices: boolean;
  }
  interface CreateViewModelApplyVM {
    ActionName?: string;
    DisplayName?: string;
    Fields?: string[];
    Name?: string;
    Type: Rainbow.Common.Enums.VMType;
  }
  interface FieldVM {
    DisplayName?: string;
    Name?: string;
    Type?: string;
  }
  interface ModelTypeVM {
    Asssembly?: string;
    DisplayName?: string;
    Fields?: Rainbow.ViewModels.Models.FieldVM[];
    FullName?: string;
    Name?: string;
  }
}
declare namespace Rainbow.ViewModels.RoleInfos {
  interface CreateRoleInfoVM extends Yunyong.Core.ViewModels.CreateVM {
    Description?: string;
    Name: string;
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
  interface DeleteRoleInfoVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface QueryRoleInfoVM extends Yunyong.Core.PagingQueryOption {
    Name?: string;
  }
  interface RoleInfoVM extends Yunyong.Core.ViewModels.VMBase {
    Description?: string;
    Name: string;
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
  interface UpdateRoleInfoVM extends Yunyong.Core.ViewModels.UpdateVM {
    Description?: string;
    Name: string;
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
}
declare namespace Rainbow.ViewModels.Tasks {
  interface NotifyQueryOption extends Yunyong.Core.PagingQueryOption {
    Type?: Rainbow.Common.Enums.NotifyType;
  }
  interface NotifyVM extends Yunyong.Core.ViewModels.VMBase {
    Title?: string;
    Type: Rainbow.Common.Enums.NotifyType;
  }
  interface TaskVM extends Yunyong.Core.ViewModels.VMBase {
    Progress: number;
    State: Rainbow.Common.Enums.TaskState;
    Title?: string;
  }
}
declare namespace Rainbow.ViewModels.Users {
  interface CreateUserVM extends Yunyong.Core.ViewModels.CreateVM {
    IsActive: boolean;
    Name?: string;
    Phone?: string;
  }
  interface DeleteUserVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface LoginResultVM {
    IsSuccess: boolean;
    Message?: string;
    UserId?: string;
  }
  interface LoginVM {
    Password?: string;
    Phone?: string;
  }
  interface QueryUserVM extends Yunyong.Core.PagingQueryOption {
    IsActive: boolean;
    Name?: string;
    Phone?: string;
  }
  interface RegisterUserVM extends Yunyong.Core.ViewModels.CreateVM {
    Name?: string;
    Phone?: string;
  }
  interface SendLoginSmsVM {
    Phone?: string;
  }
  interface SmsLoginVM {
    Phone?: string;
    SmsCode?: string;
  }
  interface UpdateUserVM extends Yunyong.Core.ViewModels.UpdateVM {
    IsActive: boolean;
    Name?: string;
  }
  interface UserLoginTrackVM {
    ExpiresTime: Date;
    SignId: string;
    UserId: string;
  }
  interface UserProfileVM extends Yunyong.Core.ViewModels.VMBase {
    AvatarUrl?: string;
    Name?: string;
    NickName?: string;
    Phone?: string;
    Roles: string[];
    UserId: string;
  }
  interface UserVM extends Yunyong.Core.ViewModels.VMBase {
    IsActive: boolean;
    Name?: string;
    Phone?: string;
  }
}
declare namespace Rainbow.ViewModels.Utils {
  interface PhoneSmsVM {
    Code?: string;
    CodeType: Rainbow.Common.Enums.TplType;
    CreateOn: Date;
    Phone?: string;
  }
  interface SendSmsRequestVM {
    CodeType: Rainbow.Common.Enums.TplType;
    Phone?: string;
  }
  interface SendSmsResultVM {
    Message?: string;
    SmsCode?: string;
    State: boolean;
  }
  interface VerfyCodeNumLimitVM {
    ErrorNum: number;
    IsLocked: boolean;
    Phone?: string;
  }
  interface VerifyingSmsCodeRequestVM {
    Code: string;
    CodeType: Rainbow.Common.Enums.TplType;
    Phone?: string;
  }
  interface VerifyingSmsCodeResultVM {
    Message?: string;
    State: boolean;
    Token: string;
  }
  interface VerifySmsSuccessVM {
    CodeType: Rainbow.Common.Enums.TplType;
    CreateOn: Date;
    Id: string;
    IsEnable: boolean;
    Phone?: string;
    Token: string;
  }
}
declare namespace System.ComponentModel.DataAnnotations {
}
declare namespace Yunyong.Core {
  interface AsyncTaskResult {
    ErrorMessage?: string;
    Status: Yunyong.Core.AsyncTaskStatus;
  }
  interface AsyncTaskTResult<T> extends Yunyong.Core.AsyncTaskResult {
    Data?: T;
  }
  interface OrderBy {
    Desc: boolean;
    Field?: string;
  }
  interface PagingList<TEntity> {
    Data?: TEntity[];
    PageIndex: number;
    PageSize: number;
    TotalCount: number;
    TotalPage: number;
  }
  interface PagingQueryOption extends Yunyong.Core.QueryOption {
    PageIndex: number;
    PageSize: number;
  }
  interface QueryOption {
    OrderBys?: Yunyong.Core.OrderBy[];
  }
}
declare namespace Yunyong.Core.ViewModels {
  interface CreateVM {
  }
  interface DeleteVM extends Yunyong.Core.ViewModels.VMBase {
  }
  interface UpdateVM extends Yunyong.Core.ViewModels.VMBase {
  }
  interface VMBase {
    Id: string;
  }
}

