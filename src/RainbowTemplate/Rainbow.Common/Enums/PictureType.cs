using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common.Enums
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PictureType
    {
        None = 0,

        Avatar = 1,

        /// <summary>
        ///     UI图片
        /// </summary>
        [Display(Name = "UI图片")]
        UiPicture = 512,

        /// <summary>
        ///     其他
        /// </summary>
        [Display(Name = "其他")]
        Misc = 1024,
    }
}
