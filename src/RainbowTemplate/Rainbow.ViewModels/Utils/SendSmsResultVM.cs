using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;

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
    /// <summary>
    ///     短信验证结果反馈
    /// </summary>
    [Display(Name = "短信验证结果反馈")]
    public class VerifyingSmsCodeResultVM
    {
        /// <summary>
        ///     成功失败
        /// </summary>
        [Display(Name = "成功失败")]
        public bool State { get; set; }

        /// <summary>
        ///     Token
        /// </summary>
        [Display(Name = "Token")]
        public Guid Token { get; set; }

        /// <summary>
        ///     提示信息
        /// </summary>
        [Display(Name = "提示信息")]
        public string Message { get; set; }
    }

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
        public string Code { get; set; }
        /// <summary>
        ///     短信类型
        /// </summary>
        [Display(Name = "短信类型")]
        public TplType CodeType { get; set; }
    }
}
