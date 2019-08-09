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

    public class EnumDisplayController:Controller
    {
        public IEnumDisplayQueryService QueryService { get; }

        public EnumDisplayController(IEnumDisplayQueryService queryService)
        {
            QueryService = queryService;
        }
        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<List<EnumDisplayVM>>))]
        public AsyncTaskTResult<List<EnumDisplayVM>> GetEnumDisplayList()
        {
            return QueryService.GetEnumDisplayList();
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<EnumDisplayVM>))]
        public AsyncTaskTResult<EnumDisplayVM> GetEnumDisplay(string name)
        {
            return QueryService.GetEnumDisplay(name);
        }

    }
}
