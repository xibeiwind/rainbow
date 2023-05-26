// tslint:disable:no-namespace
declare namespace Rainbow.Common.Enums {
  export const enum InputControlType {
    // Input
    Input = 'Input',
    // Checkbox
    Checkbox = 'Checkbox',
    // Select
    Select = 'Select',
    // FileSelect
    FileSelect = 'FileSelect',
    // Html
    Html = 'Html'
  }
  export const enum NotifyType {
    // Metting
    Metting = 'Metting'
  }
  export const enum TaskState {
  }
  export const enum TplType {
    // 登录短信
    LoginSMS = 0
  }
  export const enum UserRoleType {
    // 未知
    Unknown = 'Unknown',
    // 客户
    Customer = 'Customer',
    // 客服
    CustomerService = 'CustomerService',
    // 超级管理员
    SysAdmin = 'SysAdmin'
  }
  export const enum VMType {
    // 未设置
    None = 'None',
    // 创建
    Create = 'Create',
    // 更新
    Update = 'Update',
    // 查询
    Query = 'Query',
    // 展示
    ListDisplay = 'ListDisplay',
    // 详情
    DetailDisplay = 'DetailDisplay',
    // 删除
    Delete = 'Delete',
    // 杂项
    Misc = 'Misc'
  }
}
declare namespace System.ComponentModel.DataAnnotations {
  export const enum DataType {
    // Custom
    Custom = 0,
    // DateTime
    DateTime = 1,
    // Date
    Date = 2,
    // Time
    Time = 3,
    // Duration
    Duration = 4,
    // PhoneNumber
    PhoneNumber = 5,
    // Currency
    Currency = 6,
    // Text
    Text = 7,
    // Html
    Html = 8,
    // MultilineText
    MultilineText = 9,
    // EmailAddress
    EmailAddress = 10,
    // Password
    Password = 11,
    // Url
    Url = 12,
    // ImageUrl
    ImageUrl = 13,
    // CreditCard
    CreditCard = 14,
    // PostalCode
    PostalCode = 15,
    // Upload
    Upload = 16
  }
}
declare namespace Yunyong.Core {
  export const enum AsyncTaskStatus {
    // Success
    Success = 0,
    // Failed
    Failed = 1
  }
}
