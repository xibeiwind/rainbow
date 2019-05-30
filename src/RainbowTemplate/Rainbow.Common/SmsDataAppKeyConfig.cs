﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Common
{
    public class SmsDataAppKeyConfig
    {
        /// <summary>
        ///     发送短息验证码APPkey
        /// </summary>
        [Display(Name = "发送短息验证码APPkey")]
        public string SendSmsAppKey { get; set; }

        /// <summary>
        ///     是否真实发送短信
        /// </summary>
        [Display(Name = "是否真实发送短信")]
        public bool SendRealSms { get; set; }
    }
}
