using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;

namespace Rainbow.Models
{
    [Table(nameof(User))]
    public class User : Entity
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }
}