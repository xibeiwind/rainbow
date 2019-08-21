using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.DataFieldTypes
{
    /// <summary>
    ///     更新DataFieldType
    /// </summary>
    [Display(Name = "更新DataFieldType")]
    [BindModel("DataFieldType", VMType.Update)]
    public class UpdateDataFieldTypeVM : UpdateVM
    {
        /// <summary>
        ///     显示类型
        /// </summary>
        [Display(Name = "显示类型")]
        public string FieldTypeDisplay { get; set; }

        /// <summary>
        ///     编辑类型
        /// </summary>
        [Display(Name = "编辑类型")]
        public string FieldTypeEdit { get; set; }
    }
}