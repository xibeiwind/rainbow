using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.AttachmentImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.AttachmentImages
{
    public partial class ManageAttachmentImageQueryService : ServiceBase, IManageAttachmentImageQueryService
    {
        public ManageAttachmentImageQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        /// <summary>
        ///     获取
        /// </summary>
        [Display(Name = "获取")]
        public async Task <AttachmentImageVM> GetAsync(Guid id)
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<AttachmentImage, AttachmentImageVM>(a => a.Id == id);
        }

        /// <summary>
        ///     查询附件图片列表（分页）
        /// </summary>
        [Display(Name = "查询附件图片列表（分页）")]
        public async Task<PagingList<AttachmentImageVM>> QueryAsync(QueryAttachmentImageVM option)
        {
            await using var conn = GetConnection();

            option.OrderBys = new List<OrderBy>
            {
                new OrderBy {Field = "CreatedOn", Desc = true}
            };

            return await conn.PagingListAsync<AttachmentImage, AttachmentImageVM>(option);
        }

	}
}