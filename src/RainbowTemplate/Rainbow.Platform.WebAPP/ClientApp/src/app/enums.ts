namespace Rainbow.Common.Enums {
  export const enum NotifyType {
    // Metting
    Metting = 0
  }
  export const enum TaskState {
  }
  export const enum TplType {
    // 登录短信
    LoginSMS = 0
  }
  export const enum UserRoleType {
    // 未知
    Unknown = 0,
    // 客户
    Customer = 1,
    // 客服
    CustomerService = 16,
    // 超级管理员
    SysAdmin = 1024
  }
  export const enum VMType {
    // 未设置
    None = 0,
    // 创建
    Create = 1,
    // 更新
    Update = 2,
    // 查询
    Query = 4,
    // 展示
    Display = 8,
    // 删除
    Delete = 16,
    // 杂项
    Misc = 128
  }
}
namespace System.ComponentModel.DataAnnotations {
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
namespace Yunyong.Core {
  export const enum AsyncTaskStatus {
    // Success
    Success = 0,
    // Failed
    Failed = 1
  }
}
