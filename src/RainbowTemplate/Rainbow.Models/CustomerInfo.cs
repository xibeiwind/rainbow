using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     客户信息
    /// </summary>
    [Display(Name = "客户信息")]
    [Table(nameof(CustomerInfo))]
    public class CustomerInfo : Entity
    {
        public string OpenId { get; set; }
        public string UnionId { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
    }
}