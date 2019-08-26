using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


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
