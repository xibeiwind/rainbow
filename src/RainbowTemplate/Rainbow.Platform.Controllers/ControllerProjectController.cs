using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Rainbow.Services.ControllerProjects;
using Controller = Yunyong.Mvc.Controller;
using Rainbow.ViewModels.ControllerProjects;
using Yunyong.Core;

namespace Rainbow.Platform.Controllers
{
	/// <summary>
    ///     ControllerProject Controller
    /// </summary>
    [Display(Name = "ControllerProject Controller")]
    [ApiController]
    [Route("api/[controller]")]

    public class ControllerProjectController: Controller
	{
		/// <summary>
		///     ControllerProject Controller构造函数
		/// </summary>
        public ControllerProjectController(IControllerProjectActionService actionService, IControllerProjectQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IControllerProjectActionService ActionService { get; }
        private IControllerProjectQueryService QueryService { get; }


        /// <summary>
        ///     创建Controller项目
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="创建Controller项目")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync([FromBody]CreateControllerProjectVM vm)
        {
            return await ActionService.CreateAsync(vm);
        }

        /// <summary>
        ///     更新Controller项目
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="更新Controller项目")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateControllerProjectVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }

        /// <summary>
        ///     获取Controller项目
        /// </summary>
        [Display(Name = "获取Controller项目")]
        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(ControllerProjectVM))]
        public async Task < ControllerProjectVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取Controller项目列表
        /// </summary>
        [Display(Name = "获取Controller项目列表")]
        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(List<ControllerProjectVM>))]
        public async Task < List<ControllerProjectVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }

        /// <summary>
        ///     查询Controller项目列表（分页）
        /// </summary>
        [Display(Name = "查询Controller项目列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesDefaultResponseType(typeof(PagingList<ControllerProjectVM>))]
        public async Task<PagingList<ControllerProjectVM>> QueryAsync([FromQuery]QueryControllerProjectVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     删除Controller项目
        /// </summary>
        [Display(Name="删除Controller项目")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteControllerProjectVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}