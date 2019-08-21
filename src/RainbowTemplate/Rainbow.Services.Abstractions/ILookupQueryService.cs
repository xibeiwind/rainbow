using System.Collections.Generic;
using System.Threading.Tasks;
using Rainbow.Common;
using Rainbow.ViewModels;
using Yunyong.Core;

namespace Rainbow.Services
{
    public interface ILookupQueryService
    {
        Task<AsyncTaskTResult<IEnumerable<LookupResultVM>>> Query(LookupAttribute attr, string filter);
        AsyncTaskTResult<LookupResultVM> Query(LookupQueryVM vm);
    }
}