using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.CustomerServices
{
	/// <summary>
    ///     显示客服人员
    /// </summary>
    [Display(Name = "显示客服人员")]
	[BindModel("CustomerService", VMType.Display)]
    public class CustomerServiceVM : VMBase
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
        public String Name { get; set; }

        /// <summary>
        ///     NickName
        /// </summary>
        [Display(Name = "NickName")]
        public String NickName { get; set; }

        /// <summary>
        ///     Phone
        /// </summary>
        [Display(Name = "Phone")]
        public String Phone { get; set; }
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
