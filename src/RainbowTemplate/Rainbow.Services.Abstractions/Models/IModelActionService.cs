using System.Threading.Tasks;
using Rainbow.ViewModels.Models;

namespace Rainbow.Services.Models
{
    public interface IModelActionService
    {
        Task CreateUpdateFiles(CreateModelSuitApplyVM vm);
    }
}
