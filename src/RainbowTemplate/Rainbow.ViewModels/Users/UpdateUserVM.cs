using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    /// <summary>
    ///     更新User
    /// </summary>
    [Display(Name = "更新User")]
    [BindModel("UserInfo", VMType.Update)]
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
        /// <summary>
        ///     头像图片
        /// </summary>
        [Display(Name = "头像图片")]
        public IFormFile File { get; set; }
    }
}