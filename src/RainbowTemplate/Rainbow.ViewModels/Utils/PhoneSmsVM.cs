using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Yunyong.EventBus;

namespace Rainbow.ViewModels.Utils
{
    public class PhoneSmsVM
    {
        public string Phone { get; set; }
        public string Code { get; set; }
        public TplType CodeType { get; set; }

        public DateTime CreateOn { get; set; }
        /* public bool SendIsSuccess { get; set; }*/
    }

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
    public class VerfyCodeNumLimitVM
    {
        /// <summary>
        ///     是否限制
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        ///     手机号.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     错误次数
        /// </summary>
        public int ErrorNum { get; set; }
    }
}
