using System;

using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Utils
{
    public class VerifySmsSuccessVM
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the token.
        /// </summary>
        public Guid Token { get; set; }

        /// <summary>
        ///     验证码类型
        /// </summary>
        /// <value>
        ///     The type of the code.
        /// </value>
        public TplType CodeType { get; set; }

        /// <summary>
        ///     手机号.
        /// </summary>
        public string Phone { get; set; }

        public DateTime CreateOn { get; set; }
        public bool IsEnable { get; set; }
    }
}