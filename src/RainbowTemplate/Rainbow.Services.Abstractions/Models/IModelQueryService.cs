using Rainbow.ViewModels.Models;
using System.Collections.Generic;

namespace Rainbow.Services.Models
{
    public interface IModelQueryService
    {
        IEnumerable<ModelTypeVM> GetModelTypes();
    }
}