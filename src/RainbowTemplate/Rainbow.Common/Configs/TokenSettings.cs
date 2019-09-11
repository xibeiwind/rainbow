using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
