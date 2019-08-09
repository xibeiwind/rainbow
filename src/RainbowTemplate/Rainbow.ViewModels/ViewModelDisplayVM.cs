using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common.Enums;

namespace Rainbow.ViewModels
{
    public class ViewModelDisplayVM
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        ///     Model名称
        /// </summary>
        [Display(Name = "Model名称")]
        public string ModelName { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }

        public List<FieldDisplayVM> Fields { get; set; }
        public VMType Type { get; set; }
    }

    public class FieldDisplayVM
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }
        /// <summary>
        ///     显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }
        /// <summary>
        ///     字段类型
        /// </summary>
        [Display(Name = "字段类型")]
        public string FieldType { get; set; }
        /// <summary>
        ///     是否枚举
        /// </summary>
        [Display(Name = "是否枚举")]
        public bool IsEnum { get; set; }

        /// <summary>
        ///     控件展示类型
        /// </summary>
        [Display(Name = "控件展示类型")]
        public DataType DataType { get; set; }
        /// <summary>
        ///     是否可空
        /// </summary>
        [Display(Name = "是否可空")]
        public bool IsNullable { get; set; }

        public LookupSettingVM Lookup { get; set; }

    }

    public class LookupSettingVM
    {
        public string VMType { get; set; }
        public string DisplayField { get; set; }
        public string ValueField { get; set; }
    }

    public class ModelDisplaySuitVM
    {
        public string ModelName { get; set; }
        public string DisplayName { get; set; }
        public List<ViewModelDisplayVM> ViewModels { get; set; }
    }
}
