using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Services;
using Rainbow.ViewModels;
using Yunyong.Core;
using Yunyong.Mvc;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class LookupQueryController:Controller
    {
        public ILookupQueryService QueryService { get; }

        public LookupQueryController(ILookupQueryService queryService)
        {
            QueryService = queryService;
        }

        [HttpPost]
        [Route("Query")]
        public async Task<AsyncTaskTResult<LookupResultVM>> Query([FromBody]LookupQueryVM vm)
        {
            return QueryService.Query(vm); 
        }
    }
}
