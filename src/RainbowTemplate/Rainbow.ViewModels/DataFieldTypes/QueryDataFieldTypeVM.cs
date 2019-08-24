using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core;

namespace Rainbow.ViewModels.DataFieldTypes
{
    /// <summary>
    ///     查询DataFieldType
    /// </summary>
    [Display(Name = "查询DataFieldType")]
    [BindModel("DataFieldType", VMType.Query)]
    public class QueryDataFieldTypeVM : PagingQueryOption
    {
        /// <summary>
        ///     DataType类型
        /// </summary>
        [Display(Name = "DataType类型")]
        public DataType? Type { get; set; }

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