using Rainbow.ViewModels.AttachmentImages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.AttachmentImages
{
    public partial interface IManageAttachmentImageQueryService
    {

        /// <summary>
        ///     获取
        /// </summary>
        [Display(Name = "获取")]
        Task <AttachmentImageVM> GetAsync(Guid id);
        /// <summary>
        ///     查询附件图片列表（分页）
        /// </summary>
        [Display(Name = "查询附件图片列表（分页）")]
        Task<PagingList<AttachmentImageVM>> QueryAsync(QueryAttachmentImageVM option);
    }
}
