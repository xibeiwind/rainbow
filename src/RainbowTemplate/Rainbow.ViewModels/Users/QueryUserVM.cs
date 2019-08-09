using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.ViewModels;

using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.Users
{
	/// <summary>
    ///     查询User
    /// </summary>
    [Display(Name = "查询User")]
    public class QueryUserVM : PagingQueryOption
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
