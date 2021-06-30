using Rainbow.ViewModels.ModelTypeMetas;
using System;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.ModelTypeMetas
{
    public interface IModelTypeMetaService
    {
        Task<AsyncTaskTResult<ModelTypeMetaVM>> GetModelTypeMetaAsync(string name);

        Task<AsyncTaskTResult<Guid>> CreateUpdateAsync(CreateUpdateModelTypeMetaVM vm);
    }
}
