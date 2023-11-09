﻿using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.RoleInfos
{
    /// <summary>
    ///     创建角色
    /// </summary>
    [Display(Name = "创建角色")]
    [BindModel("RoleInfo", VMType.Create)]
    public class CreateRoleInfoVM : CreateVM
    {
        /// <summary>
        ///     角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     角色描述
        /// </summary>
        [Display(Name = "角色描述")]
        [DataType(DataType.Html)]
        public string Description { get; set; }

        /// <summary>
        ///     RoleType
        /// </summary>
        [Display(Name = "RoleType")]
        public UserRoleType RoleType { get; set; }
    }
}