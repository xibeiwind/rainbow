using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;

using SpiritBulldozer.Common;
using SpiritBulldozer.Common.Enums;


namespace SpiritBulldozer.ViewModels.Users
{
	/// <summary>
    ///     显示User
    /// </summary>
    [Display(Name = "显示User")]
    public class UserVM : VMBase
    {
		
        /// <summary>
        ///     Name
        /// </summary>
        [Display(Name = "Name")]
        public String Name { get; set; }

    }
}
