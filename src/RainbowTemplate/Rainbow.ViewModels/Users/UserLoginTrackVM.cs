using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.ViewModels.Users
{
    public class UserLoginTrackVM
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public Guid UserId { get; set; }
        /// <summary>
        ///     签名Id
        /// </summary>
        [Display(Name = "签名Id")]
        public Guid SignId { get; set; }
        /// <summary>
        ///     过期时间
        /// </summary>
        [Display(Name = "过期时间")]
        public DateTime ExpiresTime { get; set; }
    }
}
