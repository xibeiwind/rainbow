using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.ClientModules
{
    /// <summary>
    ///     删除ClientModule
    /// </summary>
    [Display(Name = "删除ClientModule")]
    [BindModel("ClientModule", VMType.Delete)]
    public class DeleteClientModuleVM : DeleteVM
    {
    }
}