using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    public class UpdateUserAvatarVM : UpdateVM
    {
        /// <summary>
        ///     头像图片
        /// </summary>
        [Display(Name = "头像图片")]
        public IFormFile File { get; set; }
    }
}