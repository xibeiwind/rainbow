using System;
using System.Threading.Tasks;

using Rainbow.ViewModels.Tasks;

using Yunyong.Core;

namespace Rainbow.Services
{
    public interface INotifyService
    {
        Guid UserId { get; set; }

        Task<PagingList<NotifyVM>> QueryAsync(NotifyQueryOption option);
        Task<NotifyVM> GetAsync(Guid id);
        Task<AsyncTaskResult> ReadedAsync(Guid id);
    }
}