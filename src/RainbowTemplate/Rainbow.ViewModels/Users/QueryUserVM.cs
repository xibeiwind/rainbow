using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     查询User
    /// </summary>
    [Display(Name = "查询User")]
    [BindModel("UserInfo", VMType.Query)]
    public class QueryUserVM : PagingQueryOption
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