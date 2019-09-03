using System.Collections.Generic;
using Rainbow.ViewModels.Services;

namespace Rainbow.Services.Services
{
    public class ServiceQueryService : IServiceQueryService
    {
        public IEnumerable<ServiceVM> ListAsync()
        {
            yield break;
        }

        public IEnumerable<ServiceVM> QueryAsync(QueryServiceVM vm)
        {
            yield break;
        }
    }
}