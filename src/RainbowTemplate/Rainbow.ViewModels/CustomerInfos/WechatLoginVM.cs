using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.CustomerInfos
{
    public class WechatLoginVM
    {
        /// <summary>
        ///     LoginCode
        /// </summary>
        [Display(Name = "LoginCode")]
        public string LoginCode { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        /// <summary>
        ///     用户头像
        /// </summary>
        [Display(Name = "用户头像")]
        public string AvatarUrl { get; set; }
    }
}