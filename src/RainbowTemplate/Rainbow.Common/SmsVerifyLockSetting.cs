using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Common
{
    public class SmsVerifyLockSetting
    {
        /// <summary>
        ///     锁定时长分钟
        /// </summary>
        [Display(Name = "锁定时长分钟")]
        public int LockMinutes { get; set; }
        /// <summary>
        ///     连续错误次数触发锁定
        /// </summary>
        [Display(Name = "连续错误次数触发锁定")]
        public int LockTriggerErrorTimes { get; set; }
    }
}
