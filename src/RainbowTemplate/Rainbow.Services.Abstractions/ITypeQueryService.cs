using System.Collections.Generic;

namespace Rainbow.Services
{
    public interface ITypeQueryService
    {
        IEnumerable<string> Query(string keyword);
    }
}