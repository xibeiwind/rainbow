using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Yunyong.Core;
using Yunyong.Core.Attributes;


namespace Rainbow.Models
{
    /// <summary>
    ///     附件图片
    /// </summary>
    [Display(Name = "附件图片")]
    [Table(nameof(AttachmentImage))]
    public class AttachmentImage : Entity
    {
        /// <summary>
        ///     目标对象Id
        /// </summary>
        [Display(Name = "目标对象Id")]
        [Required]
        public Guid TargetId { get; set; }

        /// <summary>
        ///     客户Id
        /// </summary>
        [Display(Name = "客户Id")]
        [Lookup(nameof(CustomerInfo), nameof(CustomerInfo.NickName), nameof(CustomerInfo.Id))]
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
