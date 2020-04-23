using System.Collections.Generic;

using Rainbow.ViewModels.Models;

namespace Rainbow.Services.Models
{
    public interface IModelQueryService
    {
        IEnumerable<ModelTypeVM> GetModelTypes();
    }
}