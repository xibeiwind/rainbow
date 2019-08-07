using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

using SpiritBulldozer.Common;
using SpiritBulldozer.Common.Enums;


namespace SpiritBulldozer.ViewModels.Users
{
	/// <summary>
    ///     更新User
    /// </summary>
    [Display(Name = "更新User")]
    public class UpdateUserVM : UpdateVM
    {
		
        /// <summary>
        ///     Phone
        /// </summary>
        [Display(Name = "Phone")]
        public String Phone { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        [Display(Name = "Name")]
        public String Name { get; set; }

        /// <summary>
        ///     IsActive
        /// </summary>
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

    }
}
