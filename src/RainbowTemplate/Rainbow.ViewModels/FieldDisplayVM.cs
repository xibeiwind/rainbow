﻿using System.ComponentModel.DataAnnotations;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.Utils;

namespace Rainbow.ViewModels
{
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
        ///     字段控件类型
        /// </summary>
        [Display(Name = "字段控件类型")]
        public InputControlType ControlType { get; set; }

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
}