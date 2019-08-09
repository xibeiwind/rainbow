using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.ViewModels;

using SpiritBulldozer.Common;
using SpiritBulldozer.Common.Enums;


namespace SpiritBulldozer.ViewModels.DataFieldTypes
{
	/// <summary>
    ///     查询DataFieldType
    /// </summary>
    [Display(Name = "查询DataFieldType")]
    public class QueryDataFieldTypeVM : PagingQueryOption
    {
		
        /// <summary>
        ///     DataType类型
        /// </summary>
        [Display(Name = "DataType类型")]
        public DataType Type { get; set; }

        /// <summary>
        ///     显示类型
        /// </summary>
        [Display(Name = "显示类型")]
        public String FieldTypeDisplay { get; set; }

        /// <summary>
        ///     编辑类型
        /// </summary>
        [Display(Name = "编辑类型")]
        public String FieldTypeEdit { get; set; }

    }
}
