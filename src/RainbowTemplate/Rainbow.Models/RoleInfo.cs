using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rainbow.Common.Enums;
using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     角色
    /// </summary>
    [Display(Name = "角色")]
    [Table(nameof(RoleInfo))]
    public class RoleInfo : Entity
    {
        /// <summary>
        ///     角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     角色描述
        /// </summary>
        [Display(Name = "角色描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public UserRoleType RoleType { get; set; }
    }
}