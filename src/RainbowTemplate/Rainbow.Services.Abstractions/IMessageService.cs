using System;
using System.Threading.Tasks;
using Rainbow.ViewModels.Messages;
using Yunyong.Core;

namespace Rainbow.Services
{
    public interface IMessageService
    {
        Guid UserId { get; set; }
        Task<PagingList<MessageVM>> QueryAsync(MessageQueryOption option);
        Task<AsyncTaskResult> ReadedAsync(Guid id);
        Task<MessageVM> GetAsync(Guid id);
    }
}