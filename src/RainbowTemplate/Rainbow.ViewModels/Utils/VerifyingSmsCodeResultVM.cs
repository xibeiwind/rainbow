using System;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Utils
{
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
}