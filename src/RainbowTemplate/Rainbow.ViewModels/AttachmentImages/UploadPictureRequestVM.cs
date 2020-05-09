using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.AttachmentImages
{
    /// <summary>
    ///     上传图片
    /// </summary>
    [Display(Name = "上传图片")]
    public class UploadPictureRequestVM : CreateVM
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid TargetId { get; set; }
        /// <summary>
        ///     上传图片
        /// </summary>
        [Display(Name = "上传图片")]
        public IFormFile File { get; set; }
    }

    public class UploadPictureAdvRequestVM : CreateVM
    {
        //public Guid TargetId { get; set; }
        public IFormFile Upload { get; set; }
    }

    public class UploadPictureAdvResultVM : VMBase
    {
        public string FileName { get; set; }
        public string ImageUrl { get; set; }
    }
}