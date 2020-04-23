using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     用户Profile
    /// </summary>
    [Display(Name = "用户Profile")]
    public class UserProfileVM : VMBase
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public Guid UserId { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        ///     NickName
        /// </summary>
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        /// <summary>
        ///     Phone
        /// </summary>
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        ///     头像图片地址
        /// </summary>
        [Display(Name = "头像图片地址")]
        [DataType(DataType.ImageUrl)]
        public string AvatarUrl { get; set; }

        /// <summary>
        ///     客服角色
        /// </summary>
        [Display(Name = "客服角色")]
        public IEnumerable<string> Roles { get; set; }
    }
}