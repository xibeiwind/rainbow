using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Rainbow.Common.Enums;

namespace Rainbow.ViewModels
{
    public class ViewModelDisplayVM
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        ///     Model名称
        /// </summary>
        [Display(Name = "Model名称")]
        public string ModelName { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }

        public List<FieldDisplayVM> Fields { get; set; }
        public VMType Type { get; set; }
    }
}