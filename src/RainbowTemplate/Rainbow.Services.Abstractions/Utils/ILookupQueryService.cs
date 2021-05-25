using Rainbow.ViewModels.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.Core.Attributes;

namespace Rainbow.Services.Utils
{
    public interface ILookupQueryService
    {
        Task<AsyncTaskTResult<IEnumerable<LookupResultVM>>> Query(LookupAttribute attr, string filter);
        Task<IEnumerable<LookupResultVM>> QueryAsync(LookupQueryVM vm);
        Task<LookupResultVM> GetAsync(LookupQueryVM vm);
    }
}