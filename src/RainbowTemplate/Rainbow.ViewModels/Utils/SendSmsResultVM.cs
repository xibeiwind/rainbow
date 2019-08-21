using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Utils
{
    public class SendSmsResultVM
    {
        /// <summary>
        ///     成功失败
        /// </summary>
        [Display(Name = "成功失败")]
        public bool State { get; set; }

        /// <summary>
        ///     提示信息
        /// </summary>
        [Display(Name = "提示信息")]
        public string Message { get; set; }

        /// <summary>
        ///     验证码
        /// </summary>
        [Display(Name = "验证码")]
        public string SmsCode { get; set; }
    }
}