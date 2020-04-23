using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.ClientModules
{
    /// <summary>
    ///     更新客户端模块
    /// </summary>
    [Display(Name = "更新客户端模块")]
    [BindModel("ClientModule", VMType.Update)]
    public class UpdateClientModuleVM : UpdateVM
    {
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [Required]
        public string Name { get; set; }

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