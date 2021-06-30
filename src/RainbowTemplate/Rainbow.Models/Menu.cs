using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;
using Yunyong.Core.Attributes;

namespace Rainbow.Models
{
    /// <summary>
    ///     菜单
    /// </summary>
    [Display(Name = "菜单")]
    [Table(nameof(Menu))]
    public class Menu : Entity
    {
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        ///     角色
        /// </summary>
        [Display(Name = "角色")]
        [Lookup(nameof(RoleInfo), nameof(RoleInfo.Name), nameof(RoleInfo.Id))]
        public Guid? RoleId { get; set; }
    }
}
