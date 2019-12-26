using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Utils
{
    /// <summary>
    ///     位置信息
    /// </summary>
    [Display(Name = "位置信息")]
    public class LocationVM
    {
        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        public string Address { get; set; }
        /// <summary>
        ///     经度
        /// </summary>
        [Display(Name = "经度")]
        public double Longitude { get; set; }

        /// <summary>
        ///     纬度
        /// </summary>
        [Display(Name = "纬度")]
        public double Latitude { get; set; }
    }
}
