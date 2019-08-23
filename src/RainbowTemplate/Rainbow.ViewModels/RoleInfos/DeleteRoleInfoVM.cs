using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.RoleInfos
{
	/// <summary>
    ///     删除RoleInfo
    /// </summary>
    [Display(Name = "删除RoleInfo")]
	[BindModel("RoleInfo", VMType.Delete)]
    public class DeleteRoleInfoVM : DeleteVM
    {
    }
}
