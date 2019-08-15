using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.DataFieldTypes
{
	/// <summary>
    ///     删除DataFieldType
    /// </summary>
    [Display(Name = "删除DataFieldType")]
	[BindModel("$ModelName$", VMType.Delete)]
    public class DeleteDataFieldTypeVM : DeleteVM
    {
    }
}
