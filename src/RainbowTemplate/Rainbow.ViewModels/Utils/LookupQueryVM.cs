using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Utils
{
    public class LookupQueryVM
    {
        public string TypeName { get; set; }
        /// <summary>
        ///     显示字段
        /// </summary>
        [Display(Name = "显示字段")]
        public string DisplayField { get; set; }
        /// <summary>
        ///     标记字段
        /// </summary>
        [Display(Name = "标记字段")]
        public string ValueField { get; set; }
        /// <summary>
        ///     查询条件
        /// </summary>
        [Display(Name = "查询条件")]
        public string Filter { get; set; }
    }
}