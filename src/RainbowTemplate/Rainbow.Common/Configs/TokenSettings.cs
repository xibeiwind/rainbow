using System;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common.Configs
{
    public class TokenSettings
    {
        /// <summary>
        ///     token有效期
        /// </summary>
        [Display(Name = "token有效期")]
        public TimeSpan TokenValidity { get; set; }
    }
}