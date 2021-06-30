using Microsoft.AspNetCore.Mvc;
using Rainbow.Services;
using Rainbow.ViewModels;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    /// ViewModelDisplay
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ViewModelDisplayController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryService"></param>
        public ViewModelDisplayController(IViewModelDisplayQueryService queryService)
        {
            QueryService = queryService;
        }

        private IViewModelDisplayQueryService QueryService { get; }

        [HttpGet]
        [Route("VMDisplay")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<ViewModelDisplayVM>))]
        public async Task<AsyncTaskTResult<ViewModelDisplayVM>> GetVMDisplay([FromQuery] DisplayQueryVM vm)
        {
            return await QueryService.GetVMDisplay(vm.Name);
        }

        [HttpGet]
        [Route("ModelVMDisplays")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<ModelDisplaySuitVM>))]
        public async Task<AsyncTaskTResult<ModelDisplaySuitVM>> GetModelVMDisplays([FromQuery] DisplayQueryVM vm)
        {
            return await QueryService.GetModelVMDisplays(vm.Name);
        }
    }
}
