using System;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.DataFieldTypes
{
	/// <summary>
    ///     删除DataFieldType
    /// </summary>
    [Display(Name = "删除DataFieldType")]
    [BindModel("DataFieldType", VMType.Delete)]
    public class DeleteDataFieldTypeVM : DeleteVM
    {
    }
}
