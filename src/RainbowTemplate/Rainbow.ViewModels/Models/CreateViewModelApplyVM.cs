using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Models
{
    public class CreateViewModelApplyVM
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
        ///     操作类型
        /// </summary>
        [Display(Name = "操作类型")]
        public VMType Type { get; set; }
        /// <summary>
        ///     操作名称
        /// </summary>
        [Display(Name = "操作名称")]
        public string ActionName { get; set; }
        /// <summary>
        ///     添加权限控制
        /// </summary>
        [Display(Name = "添加权限控制")]
        public bool WithAuthorize { get; set; }
        /// <summary>
        ///     授权角色
        /// </summary>
        [Display(Name = "授权角色")]
        public List<string> AuthorizeRoles { get; set; }
        public List<string> Fields { get; set; }
    }
}