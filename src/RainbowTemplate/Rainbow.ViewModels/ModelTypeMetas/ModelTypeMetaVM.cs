using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.ModelTypeMetas
{
    public class CreateUpdateModelTypeMetaVM
    {
        public string TypeName { get; set; }
        //public List<ModelFieldVM> Fields { get; set; }
        public List<ModelViewTypeMetaVM> ViewMetas { get; set; }
        //public bool CanCreate { get; set; }
        //public bool CanEdit { get; set; }
        //public bool CanDelete { get; set; }
    }
    public class ModelTypeMetaVM : VMBase
    {
        public string TypeName { get; set; }
        public List<ModelViewTypeMetaVM> ViewMetas { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }




    //public class ModelFieldVM
    //{
    //    public string Name { get; set; }
    //    public string Title { get; set; }
    //}

    /// <summary>
    ///     字段输入类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]

    public enum FieldInputType
    {
        /// <summary>
        ///     文本
        /// </summary>
        [Display(Name = "文本")]
        Text,
        /// <summary>
        ///     多行文本
        /// </summary>
        [Display(Name = "多行文本")]
        Multline,
        /// <summary>
        ///     日期
        /// </summary>
        [Display(Name = "日期")]
        Date,
        /// <summary>
        ///     日期时间
        /// </summary>
        [Display(Name = "日期时间")]
        Datetime,
        /// <summary>
        ///     数字
        /// </summary>
        [Display(Name = "数字")]
        Number,
        /// <summary>
        ///     货币
        /// </summary>
        [Display(Name = "货币")]
        Currency,
        /// <summary>
        ///     范围
        /// </summary>
        [Display(Name = "范围")]
        Range,
        /// <summary>
        ///     电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        Phone,
        /// <summary>
        ///     复选框
        /// </summary>
        [Display(Name = "复选框")]
        Checkbox,
        /// <summary>
        ///     单选框
        /// </summary>
        [Display(Name = "单选框")]
        Radio,
        /// <summary>
        ///     Rate
        /// </summary>
        [Display(Name = "Rate")]
        Rate,
        /// <summary>
        ///     下拉框
        /// </summary>
        [Display(Name = "下拉框")]
        Select,
        /// <summary>
        ///     枚举
        /// </summary>
        [Display(Name = "枚举")]
        Enum,
        /// <summary>
        ///     开关
        /// </summary>
        [Display(Name = "开关")]
        Switch,
        /// <summary>
        ///     图片
        /// </summary>
        [Display(Name = "图片")]
        Image,
        /// <summary>
        ///     网址
        /// </summary>
        [Display(Name = "网址")]
        Url,
        /// <summary>
        ///     邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        Email,
        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        Icon,
        /// <summary>
        ///     关联
        /// </summary>
        [Display(Name = "关联")]
        Lookup
    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum ViewType
    {
        /// <summary>
        ///     列表
        /// </summary>
        [Display(Name = "列表")]
        List,
        /// <summary>
        ///     详情
        /// </summary>
        [Display(Name = "详情")]
        Detail,
        /// <summary>
        ///     编辑
        /// </summary>
        [Display(Name = "编辑")]
        Edit,
        /// <summary>
        ///     创建
        /// </summary>
        [Display(Name = "创建")]
        Create
    }
}
