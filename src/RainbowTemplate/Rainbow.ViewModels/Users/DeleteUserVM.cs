using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     删除User
    /// </summary>
    [Display(Name = "删除User")]
    [BindModel("UserInfo", VMType.Delete)]
    public class DeleteUserVM : DeleteVM
    {
    }
}