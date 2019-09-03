using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.Attributes;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.DataFieldTypes
{
	/// <summary>
    ///     查询DataFieldType
    /// </summary>
    [Display(Name = "查询DataFieldType")]
	[BindModel("DataFieldType", VMType.Query)]
    public partial class QueryDataFieldTypeVM : PagingQueryOption
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
        [QueryColumn("FieldTypeDisplay", CompareEnum.Like)]
        public string FieldTypeDisplay { get; set; }

        /// <summary>
        ///     编辑类型
        /// </summary>
        [Display(Name = "编辑类型")]
        [QueryColumn("FieldTypeEdit", CompareEnum.Like)]
        public string FieldTypeEdit { get; set; }

    }
}
