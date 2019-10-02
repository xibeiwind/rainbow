using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    public class UpdateUserAvatarVM:UpdateVM
    {
        /// <summary>
        ///     头像图片
        /// </summary>
        [Display(Name = "头像图片")]
        public IFormFile File { get; set; }
    }
}
