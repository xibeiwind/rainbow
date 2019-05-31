using System.ComponentModel.DataAnnotations;
using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Utils
{
    /// <summary>
    ///     验证短信请求
    /// </summary>
    [Display(Name = "验证短信请求")]
    public class VerifyingSmsCodeRequestVM
    {
        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        /// <summary>
        ///     短信验证码
        /// </summary>
        [Display(Name = "短信验证码")]
        [Required]
        [StringLength(6, MinimumLength = 4)]
        public string Code { get; set; }
        /// <summary>
        ///     短信类型
        /// </summary>
        [Display(Name = "短信类型")]
        public TplType CodeType { get; set; }
    }
}