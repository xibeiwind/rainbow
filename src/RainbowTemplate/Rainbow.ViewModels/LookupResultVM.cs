using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels
{
    /// <summary>
    ///     Lookup搜索结果
    /// </summary>
    [Display(Name = "Lookup搜索结果")]
    public class LookupResultVM : VMBase
    {
        /// <summary>
        ///     显示文本
        /// </summary>
        [Display(Name = "显示文本")]
        public object Name { get; set; }
        /// <summary>
        ///     值
        /// </summary>
        [Display(Name = "值")]
        public object Value { get; set; }
    }
}
