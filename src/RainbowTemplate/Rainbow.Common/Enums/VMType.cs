using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common.Enums
{
    public enum VMType
    {
        /// <summary>
        ///     未设置
        /// </summary>
        [Display(Name = "未设置")]
        None,
        /// <summary>
        ///     创建
        /// </summary>
        [Display(Name = "创建")]
        Create = 1,
        /// <summary>
        ///     更新
        /// </summary>
        [Display(Name = "更新")]
        Update = 2,
        /// <summary>
        ///     查询
        /// </summary>
        [Display(Name = "查询")]
        Query = 4,
        /// <summary>
        ///     展示
        /// </summary>
        [Display(Name = "展示")]
        ListDisplay = 8,
        /// <summary>
        ///     详情
        /// </summary>
        [Display(Name = "详情")]
        DetailDisplay = 32,
        /// <summary>
        ///     删除
        /// </summary>
        [Display(Name = "删除")]
        Delete = 64,
        /// <summary>
        ///     杂项
        /// </summary>
        [Display(Name = "杂项")]
        Misc = 128,
    }

}
