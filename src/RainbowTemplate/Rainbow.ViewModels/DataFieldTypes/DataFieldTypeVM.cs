using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.DataFieldTypes
{
	/// <summary>
    ///     显示DataFieldType
    /// </summary>
    [Display(Name = "显示DataFieldType")]
    public class DataFieldTypeVM : VMBase
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
