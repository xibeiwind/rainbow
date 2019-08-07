using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.Tasks;
using Yunyong.Core;

namespace Rainbow.Services
{
    public class NotifyService : INotifyService
    {
        public Guid UserId { get; set; }

        public async Task<PagingList<NotifyVM>> QueryAsync(NotifyQueryOption option)
        {
            return new PagingList<NotifyVM>
            {
                PageIndex = option.PageIndex,
                PageSize = option.PageSize,
                TotalCount = 20,
                Data = GetDemoData(option.PageSize).ToList()
            };
        }

        public async Task<NotifyVM> GetAsync(Guid id)
        {
            var rand = new Random();
            return new NotifyVM
            {
                Id = GuidUtil.NewSequentialId(),
                Title = $"通知消息{rand.Next(100, 999)}",
                Type = NotifyType.Metting
            };
        }

        public async Task<AsyncTaskResult> ReadedAsync(Guid id)
        {
            return new AsyncTaskResult();
        }

        private IEnumerable<NotifyVM> GetDemoData(int count)
        {
            var rand = new Random();

            for (var i = 0; i < count; i++)
            {
                yield return new NotifyVM
                {
                    Id = GuidUtil.NewSequentialId(),
                    Title = $"通知消息{rand.Next(100, 999)}",
                    Type = NotifyType.Metting
                };
            }
        }
    }
}