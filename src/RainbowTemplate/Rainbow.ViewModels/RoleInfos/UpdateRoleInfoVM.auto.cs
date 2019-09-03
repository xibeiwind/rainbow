﻿using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.RoleInfos
{
	/// <summary>
    ///     更新角色
    /// </summary>
    [Display(Name = "更新角色")]
	[BindModel("RoleInfo", VMType.Update)]
    public partial class UpdateRoleInfoVM : UpdateVM
    {
		
        /// <summary>
        ///     角色名称
        /// </summary>
        [Display(Name = "角色名称"),Required]
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