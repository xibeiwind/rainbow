using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rainbow.Services.Abstractions;
using Rainbow.ViewModels.Messages;
using Yunyong.Core;

namespace Rainbow.Services
{
    public class MessageService : IMessageService
    {
        public Guid UserId { get; set; }

        public async Task<PagingList<MessageVM>> QueryAsync(MessageQueryOption option)
        {
            return new PagingList<MessageVM>
            {
                PageIndex = option.PageIndex,
                PageSize = option.PageSize,
                TotalCount = 20,
                Data = GetDemoData(option.PageSize).ToList()
            };
        }

        public async Task<AsyncTaskResult> ReadedAsync(Guid msgId)
        {
            return new AsyncTaskResult();
        }

        public async Task<MessageVM> GetAsync(Guid msgId)
        {
            var rand = new Random();

            return new MessageVM
            {
                Id = GuidUtil.NewSequentialId(),
                SendFrom = "发件人",
                SendFromId = GuidUtil.NewSequentialId(),
                SendOn = DateTime.Now.AddMinutes(-rand.Next(10)),
                SendToId = UserId,
                SendTo = "收件人",
                Title = $"测试发送消息{rand.Next(100, 999)}",
                Summary = $"测试内容{rand.Next(1000, 9999)}"
            };
        }

        private IEnumerable<MessageVM> GetDemoData(int count)
        {
            var rand = new Random();

            for (var i = 0; i < count; i++)
            {
                yield return new MessageVM
                {
                    Id = GuidUtil.NewSequentialId(),
                    SendFrom = "发件人",
                    SendFromId = GuidUtil.NewSequentialId(),
                    SendOn = DateTime.Now.AddMinutes(-i),
                    SendToId = UserId,
                    SendTo = "收件人",
                    Title = $"测试发送消息{rand.Next(100, 999)}",
                    Summary = $"测试内容{rand.Next(1000, 9999)}"
                };
            }
        }
    }
}