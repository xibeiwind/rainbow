using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Models;
using Rainbow.ViewModels.Models;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    ///     模型服务
    /// </summary>
    [Display(Name = "模型服务")]
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : Controller
    {
        public ModelController(IModelActionService actionService, IModelQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        public IModelActionService ActionService { get; }
        public IModelQueryService QueryService { get; }

        [HttpPost]
        [Route("CreateUpdate")]
        [ProducesDefaultResponseType(typeof(bool))]
        public Task CreateUpdateFiles(CreateModelSuitApplyVM vm)
        {
            return ActionService.CreateUpdateFiles(vm);
        }

        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ModelTypeVM>))]
        public IEnumerable<ModelTypeVM> GetModelTypes()
        {
            return QueryService.GetModelTypes();
        }
    }
}