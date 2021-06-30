using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Models;
using Rainbow.ViewModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionService"></param>
        /// <param name="queryService"></param>
        public ModelController(IModelActionService actionService, IModelQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IModelActionService ActionService { get; }
        private IModelQueryService QueryService { get; }

        /// <summary>
        ///     创建更新设置
        /// </summary>
        [Display(Name = "创建更新设置")]
        [HttpPost]
        [Route("CreateUpdate")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<AsyncTaskTResult<bool>> CreateUpdateFiles(CreateModelSuitApplyVM vm)
        {
            return await ActionService.CreateUpdateFiles(vm);
        }
        /// <summary>
        ///     获取model列表
        /// </summary>
        [Display(Name = "获取model列表")]
        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(IEnumerable<ModelTypeVM>))]
        public IEnumerable<ModelTypeVM> GetModelTypes()
        {
            return QueryService.GetModelTypes();
        }

        /// <summary>
        ///     重新生成TS代码
        /// </summary>
        [Display(Name = "重新生成TS代码")]
        [HttpPost]
        [Route("RegenerateTsCode")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<bool>))]
        public async Task<AsyncTaskTResult<bool>> RegenerateTsCode()
        {
            return await ActionService.RegenerateTsCode();
        }

        /// <summary>
        ///     更新AppRouting
        /// </summary>
        [Display(Name = "更新AppRouting")]
        [HttpPost]
        [Route("UpdateAppRoutingModule")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<bool>))]

        public async Task<AsyncTaskTResult<bool>> UpdateAppRoutingModule()
        {
            return await ActionService.UpdateAppRoutingModule();
        }
    }
}