using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;

namespace Rainbow.Models
{
    [Table(nameof(UserInfo))]
    public class UserInfo : Entity
    {
        [Required] public string Phone { get; set; }

        [Required] public string Name { get; set; }

        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
    }
}