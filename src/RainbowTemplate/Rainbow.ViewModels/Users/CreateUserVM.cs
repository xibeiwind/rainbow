using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     创建User
    /// </summary>
    [Display(Name = "创建User")]
    public class CreateUserVM : CreateVM
    {
        /// <summary>
        ///     Phone
        /// </summary>
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        ///     IsActive
        /// </summary>
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}