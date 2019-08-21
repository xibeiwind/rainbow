using System.Threading.Tasks;
using Rainbow.ViewModels.Models;
using Yunyong.Core;

namespace Rainbow.Services.Models
{
    public interface IModelActionService
    {
        Task<AsyncTaskTResult<bool>> CreateUpdateFiles(CreateModelSuitApplyVM vm);

        Task<AsyncTaskTResult<bool>> RegenerateTsCode();
    }
}