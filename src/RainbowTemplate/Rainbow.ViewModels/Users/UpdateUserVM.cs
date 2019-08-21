using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     更新User
    /// </summary>
    [Display(Name = "更新User")]
    public class UpdateUserVM : UpdateVM
    {
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