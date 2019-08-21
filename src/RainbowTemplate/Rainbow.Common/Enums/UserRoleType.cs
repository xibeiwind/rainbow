using System;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common.Enums
{
    [Flags]
    public enum UserRoleType
    {
        /// <summary>
        ///     未知
        /// </summary>
        [Display(Name = "未知")] Unknown = 0,

        /// <summary>
        ///     客户
        /// </summary>
        [Display(Name = "客户")] Customer = 1,

        /// <summary>
        ///     客服
        /// </summary>
        [Display(Name = "客服")] CustomerService = 16,

        /// <summary>
        ///     超级管理员
        /// </summary>
        [Display(Name = "超级管理员")] SysAdmin = 1024
    }
}