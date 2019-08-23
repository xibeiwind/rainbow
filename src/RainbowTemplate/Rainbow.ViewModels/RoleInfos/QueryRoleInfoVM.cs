using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.RoleInfos
{
	/// <summary>
    ///     查询角色
    /// </summary>
    [Display(Name = "查询角色")]
	[BindModel("RoleInfo", VMType.Query)]
    public class QueryRoleInfoVM : PagingQueryOption
    {
		
        /// <summary>
        ///     角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        public String Name {get;set;}

    }
}
