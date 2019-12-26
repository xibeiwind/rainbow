// tslint:disable:no-empty-interface
// tslint:disable:no-namespace

declare namespace Microsoft.AspNetCore.Http {
  interface IFormFile {
    // ContentDisposition
    ContentDisposition?: string;
    // ContentType
    ContentType?: string;
    // FileName
    FileName?: string;
    // Headers
    Headers: System.Collections.Generic.KeyValuePair<string, string[]>[];
    // Length
    Length: number;
    // Name
    Name?: string;
  }
}
declare namespace Rainbow.Common.Enums {
}
declare namespace Rainbow.ViewModels {
  interface DisplayQueryVM {
    // Name
    Name?: string;
  }
  interface EnumDisplayVM {
    // DisplayName
    DisplayName?: string;
    // Fields
    Fields: Rainbow.ViewModels.EnumFieldDisplayVM[];
    // FullName
    FullName?: string;
    // Name
    Name?: string;
  }
  interface EnumFieldDisplayVM {
    // DisplayName
    DisplayName?: string;
    // Name
    Name?: string;
    // Value
    Value: number;
  }
  interface FieldDisplayVM {
    // 字段控件类型
    ControlType: Rainbow.Common.Enums.InputControlType;
    // 控件展示类型
    DataType: System.ComponentModel.DataAnnotations.DataType;
    // 显示名称
    DisplayName?: string;
    // 字段类型
    FieldType?: string;
    // 是否枚举
    IsEnum: boolean;
    // 是否可空
    IsNullable: boolean;
    // Lookup
    Lookup?: Rainbow.ViewModels.Utils.LookupSettingVM;
    // 名称
    Name?: string;
  }
  interface ModelDisplaySuitVM {
    // DisplayName
    DisplayName?: string;
    // ModelName
    ModelName?: string;
    // ViewModels
    ViewModels?: Rainbow.ViewModels.ViewModelDisplayVM[];
  }
  interface ViewModelDisplayVM {
    // 显示名称
    DisplayName?: string;
    // Fields
    Fields?: Rainbow.ViewModels.FieldDisplayVM[];
    // Model名称
    ModelName?: string;
    // 名称
    Name?: string;
    // Type
    Type: Rainbow.Common.Enums.VMType;
  }
}
declare namespace Rainbow.ViewModels.ClientModules {
  interface ClientModuleVM extends Yunyong.Core.ViewModels.VMBase {
    // 是否扩展样式
    IsCustomLayout: boolean;
    // 模块名称
    Name: string;
    // 路由路径
    Path: string;
    // Title
    Title?: string;
  }
  interface CreateClientModuleVM extends Yunyong.Core.ViewModels.CreateVM {
    // 是否扩展样式
    IsCustomLayout: boolean;
    // 模块名称
    Name: string;
    // 路由路径
    Path: string;
    // Title
    Title?: string;
  }
  interface DeleteClientModuleVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface QueryClientModuleVM extends Yunyong.Core.PagingQueryOption {
    // 是否扩展样式
    IsCustomLayout?: boolean;
    // 模块名称
    Name?: string;
    // 路由路径
    Path?: string;
    // Title
    Title?: string;
  }
  interface UpdateClientModuleVM extends Yunyong.Core.ViewModels.UpdateVM {
    // 是否扩展样式
    IsCustomLayout: boolean;
    // 模块名称
    Name: string;
    // Title
    Title?: string;
  }
}
declare namespace Rainbow.ViewModels.ControllerProjects {
  interface ControllerProjectVM extends Yunyong.Core.ViewModels.VMBase {
    // 是否默认项目
    IsDefault: boolean;
    // 项目描述
    ProjectDescription?: string;
    // 项目名称
    ProjectName: string;
  }
  interface CreateControllerProjectVM extends Yunyong.Core.ViewModels.CreateVM {
    // 是否默认项目
    IsDefault: boolean;
    // 项目描述
    ProjectDescription?: string;
    // 项目名称
    ProjectName: string;
  }
  interface DeleteControllerProjectVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface QueryControllerProjectVM extends Yunyong.Core.PagingQueryOption {
    // 是否默认项目
    IsDefault?: boolean;
    // 项目名称
    ProjectName?: string;
  }
  interface UpdateControllerProjectVM extends Yunyong.Core.ViewModels.UpdateVM {
    // 是否默认项目
    IsDefault: boolean;
    // 项目描述
    ProjectDescription?: string;
    // 项目名称
    ProjectName: string;
  }
}
declare namespace Rainbow.ViewModels.Controllers {
  interface ControllerAction {
    // Argument
    Argument?: string;
    // Description
    Description?: string;
    // Name
    Name?: string;
    // ReturnType
    ReturnType?: string;
  }
  interface ControllerVM {
    // Description
    Description?: string;
    // Name
    Name?: string;
  }
}
declare namespace Rainbow.ViewModels.CustomerInfos {
  interface CustomerInfoVM {
  }
  interface CustomerLoginTrackVM {
    // 用户Id
    CustomerId: string;
    // 过期时间
    ExpiresTime: Date;
    // 签名Id
    SignId: string;
  }
  interface WechatLoginResultVM {
    // 出错信息
    ErrorMessage?: string;
    // 是否成功
    IsSuccess: boolean;
    // 如果成功返回token
    Token?: string;
  }
  interface WechatLoginVM {
    // 用户头像
    AvatarUrl?: string;
    // LoginCode
    LoginCode?: string;
    // 昵称
    NickName?: string;
  }
}
declare namespace Rainbow.ViewModels.Messages {
  interface MessageQueryOption extends Yunyong.Core.PagingQueryOption {
  }
  interface MessageVM extends Yunyong.Core.ViewModels.VMBase {
    // SendFrom
    SendFrom?: string;
    // SendFromId
    SendFromId: string;
    // SendOn
    SendOn: Date;
    // SendTo
    SendTo?: string;
    // SendToId
    SendToId: string;
    // Summary
    Summary?: string;
    // Title
    Title?: string;
  }
}
declare namespace Rainbow.ViewModels.Models {
  interface CreateModelSuitApplyVM {
    // 权限允许角色
    AuthorizeRoles?: string[];
    // Controller项目名称
    ControllerProjectName?: string;
    // Controller是否要权限控制
    ControllerWithAuthorize: boolean;
    // 生成删除VM
    EnableDelete: boolean;
    // 目录名称
    FolderName?: string;
    // 生成Controller
    GenerateController: boolean;
    // 生成Angular详情组件
    GenerateNgDetailComponent: boolean;
    // Angular组件为List组件
    GenerateNgListComponent: boolean;
    // 生成Angular组件页面
    GenerateNgModuleComponent: boolean;
    // 生成服务
    GenerateService: boolean;
    // GenerateVM
    GenerateVM: boolean;
    // Items
    Items?: Rainbow.ViewModels.Models.CreateViewModelApplyVM[];
    // 管理服务功能
    ManageService: boolean;
    // Model全名
    ModelFullName?: string;
    // Model名称
    ModelName?: string;
    // 所采用NgModule的名称
    NgModuleName?: string;
    // 跟踪操作记录
    TrackOperation: boolean;
    // 是否更新呢生成TsService
    UpdateTsServices: boolean;
  }
  interface CreateViewModelApplyVM {
    // 操作名称
    ActionName?: string;
    // 授权角色
    AuthorizeRoles?: string[];
    // 生成ControllerAction
    CreateAction: boolean;
    // 显示名称
    DisplayName?: string;
    // Fields
    Fields?: string[];
    // 名称
    Name?: string;
    // 选择生成VM
    SelectGenerateVM: boolean;
    // 操作类型
    Type: Rainbow.Common.Enums.VMType;
    // 添加权限控制
    WithAuthorize: boolean;
  }
  interface FieldVM {
    // DisplayName
    DisplayName?: string;
    // Name
    Name?: string;
    // Type
    Type?: string;
  }
  interface ModelTypeVM {
    // Asssembly
    Asssembly?: string;
    // DisplayName
    DisplayName?: string;
    // Fields
    Fields?: Rainbow.ViewModels.Models.FieldVM[];
    // FullName
    FullName?: string;
    // Name
    Name?: string;
  }
}
declare namespace Rainbow.ViewModels.RoleInfos {
  interface CreateRoleInfoVM extends Yunyong.Core.ViewModels.CreateVM {
    // 角色描述
    Description?: string;
    // 角色名称
    Name: string;
    // RoleType
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
  interface DeleteRoleInfoVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface QueryRoleInfoVM extends Yunyong.Core.PagingQueryOption {
    // 角色名称
    Name?: string;
  }
  interface RoleInfoVM extends Yunyong.Core.ViewModels.VMBase {
    // 角色描述
    Description?: string;
    // 角色名称
    Name: string;
    // RoleType
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
  interface UpdateRoleInfoVM extends Yunyong.Core.ViewModels.UpdateVM {
    // 角色描述
    Description?: string;
    // 角色名称
    Name: string;
    // RoleType
    RoleType: Rainbow.Common.Enums.UserRoleType;
  }
}
declare namespace Rainbow.ViewModels.Tasks {
  interface NotifyQueryOption extends Yunyong.Core.PagingQueryOption {
    // Type
    Type?: Rainbow.Common.Enums.NotifyType;
  }
  interface NotifyVM extends Yunyong.Core.ViewModels.VMBase {
    // Title
    Title?: string;
    // Type
    Type: Rainbow.Common.Enums.NotifyType;
  }
  interface TaskVM extends Yunyong.Core.ViewModels.VMBase {
    // Progress
    Progress: number;
    // State
    State: Rainbow.Common.Enums.TaskState;
    // Title
    Title?: string;
  }
}
declare namespace Rainbow.ViewModels.Users {
  interface CreateUserVM extends Yunyong.Core.ViewModels.CreateVM {
    // IsActive
    IsActive: boolean;
    // Name
    Name?: string;
    // Phone
    Phone?: string;
  }
  interface DeleteUserVM extends Yunyong.Core.ViewModels.DeleteVM {
  }
  interface LoginResultVM {
    // IsSuccess
    IsSuccess: boolean;
    // Message
    Message?: string;
    // UserId
    UserId?: string;
  }
  interface LoginVM {
    // Password
    Password?: string;
    // Phone
    Phone?: string;
  }
  interface QueryUserVM extends Yunyong.Core.PagingQueryOption {
    // IsActive
    IsActive: boolean;
    // Name
    Name?: string;
    // Phone
    Phone?: string;
  }
  interface RegisterUserVM extends Yunyong.Core.ViewModels.CreateVM {
    // 名称
    Name?: string;
    // 电话
    Phone?: string;
  }
  interface SendLoginSmsVM {
    // Phone
    Phone?: string;
  }
  interface SmsLoginVM {
    // Phone
    Phone?: string;
    // SmsCode
    SmsCode?: string;
  }
  interface UpdateUserAvatarVM extends Yunyong.Core.ViewModels.UpdateVM {
    // 头像图片
    File: Microsoft.AspNetCore.Http.IFormFile;
  }
  interface UpdateUserVM extends Yunyong.Core.ViewModels.UpdateVM {
    // 头像图片
    File: Microsoft.AspNetCore.Http.IFormFile;
    // IsActive
    IsActive: boolean;
    // Name
    Name?: string;
  }
  interface UserLoginTrackVM {
    // 过期时间
    ExpiresTime: Date;
    // 签名Id
    SignId: string;
    // 用户Id
    UserId: string;
  }
  interface UserProfileVM extends Yunyong.Core.ViewModels.VMBase {
    // 头像图片地址
    AvatarUrl?: string;
    // Name
    Name?: string;
    // NickName
    NickName?: string;
    // Phone
    Phone?: string;
    // 客服角色
    Roles: string[];
    // 用户Id
    UserId: string;
  }
  interface UserVM extends Yunyong.Core.ViewModels.VMBase {
    // IsActive
    IsActive: boolean;
    // Name
    Name?: string;
    // Phone
    Phone?: string;
  }
}
declare namespace Rainbow.ViewModels.Utils {
  interface LocationVM {
    // 详细地址
    Address?: string;
    // 纬度
    Latitude: number;
    // 经度
    Longitude: number;
  }
  interface LookupQueryVM {
    // 显示字段
    DisplayField?: string;
    // 查询条件
    Filter?: string;
    // TypeName
    TypeName?: string;
    // 标记字段
    ValueField?: string;
  }
  interface LookupResultVM extends Yunyong.Core.ViewModels.VMBase {
    // 显示文本
    Name?: any;
    // 值
    Value?: any;
  }
  interface LookupSettingVM {
    // DisplayField
    DisplayField?: string;
    // TypeName
    TypeName?: string;
    // ValueField
    ValueField?: string;
  }
  interface PhoneSmsVM {
    // Code
    Code?: string;
    // CodeType
    CodeType: Rainbow.Common.Enums.TplType;
    // CreateOn
    CreateOn: Date;
    // Phone
    Phone?: string;
  }
  interface SendSmsRequestVM {
    // CodeType
    CodeType: Rainbow.Common.Enums.TplType;
    // Phone
    Phone?: string;
  }
  interface SendSmsResultVM {
    // 提示信息
    Message?: string;
    // 验证码
    SmsCode?: string;
    // 成功失败
    State: boolean;
  }
  interface VerfyCodeNumLimitVM {
    // ErrorNum
    ErrorNum: number;
    // IsLocked
    IsLocked: boolean;
    // Phone
    Phone?: string;
  }
  interface VerifyingSmsCodeRequestVM {
    // 短信验证码
    Code: string;
    // 短信类型
    CodeType: Rainbow.Common.Enums.TplType;
    // 手机号码
    Phone?: string;
  }
  interface VerifyingSmsCodeResultVM {
    // 提示信息
    Message?: string;
    // 成功失败
    State: boolean;
    // Token
    Token: string;
  }
  interface VerifySmsSuccessVM {
    // CodeType
    CodeType: Rainbow.Common.Enums.TplType;
    // CreateOn
    CreateOn: Date;
    // Id
    Id: string;
    // IsEnable
    IsEnable: boolean;
    // Phone
    Phone?: string;
    // Token
    Token: string;
  }
}
declare namespace System.Collections.Generic {
  interface KeyValuePair<TKey, TValue> {
    // Key
    Key?: TKey;
    // Value
    Value?: TValue;
  }
}
declare namespace System.ComponentModel.DataAnnotations {
}
declare namespace Yunyong.Core {
  interface AsyncTaskResult {
    // ErrorMessage
    ErrorMessage?: string;
    // Status
    Status: Yunyong.Core.AsyncTaskStatus;
  }
  interface AsyncTaskTResult<T> extends Yunyong.Core.AsyncTaskResult {
    // Data
    Data?: T;
  }
  interface OrderBy {
    // Desc
    Desc: boolean;
    // Field
    Field?: string;
  }
  interface PagingList<TEntity> {
    // Data
    Data?: TEntity[];
    // PageIndex
    PageIndex: number;
    // PageSize
    PageSize: number;
    // TotalCount
    TotalCount: number;
    // TotalPage
    TotalPage: number;
  }
  interface PagingQueryOption extends Yunyong.Core.QueryOption {
    // PageIndex
    PageIndex: number;
    // PageSize
    PageSize: number;
  }
  interface QueryOption {
    // OrderBys
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
    // Id
    Id: string;
  }
}

