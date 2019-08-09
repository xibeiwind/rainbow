using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

using Rainbow.Common;
using Rainbow.Common.Enums;


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
        public String Name { get; set; }

        /// <summary>
        ///     IsActive
        /// </summary>
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

    }
}
