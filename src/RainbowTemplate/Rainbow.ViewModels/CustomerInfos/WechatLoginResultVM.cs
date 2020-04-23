using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.CustomerInfos
{
    /// <summary>
    ///     微信登录结果
    /// </summary>
    [Display(Name = "微信登录结果")]
    public class WechatLoginResultVM
    {
        /// <summary>
        ///     是否成功
        /// </summary>
        [Display(Name = "是否成功")]
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     如果成功返回token
        /// </summary>
        [Display(Name = "如果成功返回token")]
        public string Token { get; set; }

        /// <summary>
        ///     出错信息
        /// </summary>
        [Display(Name = "出错信息")]
        public string ErrorMessage { get; set; }
    }
}