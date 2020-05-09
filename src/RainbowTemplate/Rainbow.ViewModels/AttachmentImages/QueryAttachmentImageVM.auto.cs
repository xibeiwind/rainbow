using System;
using System.ComponentModel.DataAnnotations;

using Rainbow.Common;
using Rainbow.Common.Enums;

using Yunyong.Core;

namespace Rainbow.ViewModels.AttachmentImages
{
    /// <summary>
    ///     查询附件图片
    /// </summary>
    [Display(Name = "查询附件图片")]
	[BindModel("AttachmentImage", VMType.Query)]
    public partial class QueryAttachmentImageVM : PagingQueryOption
    {
        /// <summary>
        ///     图片编号
        /// </summary>
        [Display(Name = "图片编号")]
        public Guid? Id { get; set; }
        /// <summary>
        ///     客户Id
        /// </summary>
        [Display(Name = "客户Id")]
        public Guid? CustomerId { get; set; }

        /// <summary>
        ///     是否已上传至云
        /// </summary>
        [Display(Name = "是否已上传至云")]
        public bool? IsUploaded { get; set; }

    }
}
