using Microsoft.AspNetCore.Mvc;
using Rainbow.Services;
using Rainbow.ViewModels;
using System.Collections.Generic;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class EnumDisplayController:Controller
    {
        private IEnumDisplayQueryService QueryService { get; }

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
