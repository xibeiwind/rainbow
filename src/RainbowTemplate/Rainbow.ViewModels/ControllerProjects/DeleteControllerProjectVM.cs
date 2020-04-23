using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.ControllerProjects
{
    /// <summary>
    ///     删除ControllerProject
    /// </summary>
    [Display(Name = "删除ControllerProject")]
    [BindModel("ControllerProject", VMType.Delete)]
    public class DeleteControllerProjectVM : DeleteVM
    {
    }
}