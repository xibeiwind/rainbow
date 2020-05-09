using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.AttachmentImages
{
    /// <summary>
    ///     强制更新图片到云
    /// </summary>
    [Display(Name = "强制更新图片到云")]
    public class UploadPictureFileToCloudRequestVM
    {
        /// <summary>
        ///     要上传的图片
        /// </summary>
        [Display(Name = "要上传的图片")]
        public List<Guid> ImageIds { get; set; }
    }
}
