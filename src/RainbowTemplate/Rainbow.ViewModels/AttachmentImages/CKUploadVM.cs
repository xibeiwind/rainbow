using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Rainbow.ViewModels.AttachmentImages
{
    /// <summary>
    ///     CKEditor控件上传图片
    /// </summary>
    [Display(Name = "CKEditor控件上传图片")]
    public class CKUploadVM
    {
        /// <summary>
        ///     文件
        /// </summary>
        [Display(Name = "文件")]
        public IFormFile Upload { get; set; }
    }
}
