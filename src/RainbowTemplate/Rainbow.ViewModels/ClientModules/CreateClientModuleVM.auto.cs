using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.ClientModules
{
    /// <summary>
    ///     创建客户端模块
    /// </summary>
    [Display(Name = "创建客户端模块")]
    [BindModel("ClientModule", VMType.Create)]
    public class CreateClientModuleVM : CreateVM
    {
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     路由路径
        /// </summary>
        [Display(Name = "路由路径")]
        [Required]
        public string Path { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        ///     是否扩展样式
        /// </summary>
        [Display(Name = "是否扩展样式")]
        public bool IsCustomLayout { get; set; }
    }
}