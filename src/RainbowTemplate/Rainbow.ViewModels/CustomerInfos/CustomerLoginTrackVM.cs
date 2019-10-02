using System;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.CustomerInfos
{
    public class CustomerLoginTrackVM
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public Guid CustomerId { get; set; }

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
