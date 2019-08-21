using System.Threading.Tasks;
using Rainbow.ViewModels;
using Yunyong.Core;

namespace Rainbow.Services
{
    public interface IViewModelDisplayQueryService
    {
        Task<AsyncTaskTResult<ViewModelDisplayVM>> GetVMDisplay(string name);


        Task<AsyncTaskTResult<ModelDisplaySuitVM>> GetModelVMDisplays(string modelName);
    }
}