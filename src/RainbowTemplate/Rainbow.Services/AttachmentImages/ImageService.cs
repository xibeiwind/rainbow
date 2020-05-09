using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.AttachmentImages;

using Yunyong.Cache.Abstractions;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.AttachmentImages
{
    public class ImageService : ServiceBase, IImageService
    {
        /// <summary>
        /// </summary>
        public ImageService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            ICacheService<AttachmentImageVM> cacheService,
            IEventBus eventBus) : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            CacheService = cacheService;
        }

        private ICacheService<AttachmentImageVM> CacheService { get; }

        #region Implementation of IImageService

        public async Task<AttachmentImageVM> GetAsync(Guid imageId)
        {
            var item = CacheService.GetOrDefault<AttachmentImageVM>(imageId.ToString("D"));
            if (item == null)
            {
                await using var conn = GetConnection();
                item = await conn.FirstOrDefaultAsync<AttachmentImage, AttachmentImageVM>(a => a.Id == imageId);
                if (item != null) CacheService.Set(imageId.ToString("D"), item);
            }

            return item;
        }

        #endregion
    }
}