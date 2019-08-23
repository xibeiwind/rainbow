using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;

namespace Rainbow.Models
{
    [Table(nameof(UserInfo))]
    public class UserInfo : Entity
    {
        /// <summary>
        ///     电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        ///     账户名
        /// </summary>
        [Display(Name = "账户名")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     是否已激活
        /// </summary>
        [Display(Name = "是否已激活")]
        public bool IsActive { get; set; }

        /// <summary>
        ///     密码加密值
        /// </summary>
        [Display(Name = "密码加密值")]
        public string PasswordHash { get; set; }

        /// <summary>
        ///     头像图片地址
        /// </summary>
        [Display(Name = "头像图片地址")]
        [DataType(DataType.ImageUrl)]
        public string AvatarUrl { get; set; }
    }
}