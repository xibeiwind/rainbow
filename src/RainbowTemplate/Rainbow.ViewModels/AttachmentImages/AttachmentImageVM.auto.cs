using System;
using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.AttachmentImages
{
    /// <summary>
    ///     
    /// </summary>
    [Display(Name = "")]
	[BindModel("AttachmentImage", VMType.DetailDisplay)]
    public partial class AttachmentImageVM: VMBase
    {
		
        /// <summary>
        ///     目标对象Id
        /// </summary>
        [Display(Name = "目标对象Id"),Required]
        public Guid TargetId { get; set; }

        /// <summary>
        ///     客户Id
        /// </summary>
        [Display(Name = "客户Id")]
        public Guid CustomerId { get; set; }

        /// <summary>
        ///     是否已上传至云
        /// </summary>
        [Display(Name = "是否已上传至云")]
        public bool IsUploaded { get; set; }
        
        /// <summary>
        ///     图片描述
        /// </summary>
        [Display(Name = "图片描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        /// <summary>
        ///     文件名称
        /// </summary>
        [Display(Name = "文件名称")]
        [DataType(DataType.ImageUrl)]
        public string FileName { get; set; }

        /// <summary>
        ///     云端图片地址
        /// </summary>
        [Display(Name = "云端图片地址")]
        [DataType(DataType.ImageUrl)]
        public string CloudPictureUrl { get; set; }

    }
}
