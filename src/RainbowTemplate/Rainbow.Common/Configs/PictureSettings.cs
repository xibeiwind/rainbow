using Rainbow.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common.Configs
{
    public class PictureSettings
    {        /// <summary>
        ///     图片类型路径设置
        /// </summary>
        [Display(Name = "图片类型路径设置")]
        public Dictionary<PictureType, string> PictureTypePathDir { get; set; }

    }
}
