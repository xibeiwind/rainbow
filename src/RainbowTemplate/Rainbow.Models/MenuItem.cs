using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;
using Yunyong.Core.Attributes;

namespace Rainbow.Models
{
    /// <summary>
    ///     菜单项
    /// </summary>
    [Display(Name = "菜单项")]
    [Table(nameof(MenuItem))]
    public class MenuItem : Entity
    {
        /// <summary>
        ///     标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }
        /// <summary>
        ///     链接
        /// </summary>
        [Display(Name = "链接")]
        public string LinkUrl { get; set; }
        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        public string Icon { get; set; }
        /// <summary>
        ///     父级菜单
        /// </summary>
        [Display(Name = "父级菜单")]
        [Lookup(nameof(MenuItem), nameof(MenuItem.Title), nameof(MenuItem.Id))]
        public Guid? ParentId { get; set; }
        /// <summary>
        ///     主菜单Id
        /// </summary>
        [Display(Name = "主菜单Id")]
        public Guid MenuId { get; set; }
    }
}
