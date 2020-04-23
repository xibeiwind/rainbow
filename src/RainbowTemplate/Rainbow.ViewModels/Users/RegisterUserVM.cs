using System.ComponentModel.DataAnnotations;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    public class RegisterUserVM : CreateVM
    {
        /// <summary>
        ///     电话
        /// </summary>
        [Display(Name = "电话")]
        public string Phone { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }
    }
}