using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     显示User
    /// </summary>
    [Display(Name = "显示User")]
    [BindModel("UserInfo", VMType.ListDisplay)]
    public class UserVM : VMBase
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