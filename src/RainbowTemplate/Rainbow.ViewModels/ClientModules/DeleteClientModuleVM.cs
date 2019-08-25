using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


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
