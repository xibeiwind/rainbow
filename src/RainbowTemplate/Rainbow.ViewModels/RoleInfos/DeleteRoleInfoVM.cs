using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

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