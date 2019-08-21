using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rainbow.Models
{
    /// <summary>
    ///     用户角色关联
    /// </summary>
    [Display(Name = "用户角色关联")]
    [Table(nameof(UserRole))]
    public class UserRole
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        ///     角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        [Required]
        public Guid RoleId { get; set; }
    }
}