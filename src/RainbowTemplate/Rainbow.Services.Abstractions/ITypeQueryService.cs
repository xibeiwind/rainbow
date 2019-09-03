using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Services
{
    public interface ITypeQueryService
    {
        IEnumerable<string> Query(string keyword);
    }
}
