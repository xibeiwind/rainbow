using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Rainbow.Services.ClientModules;
using Controller = Yunyong.Mvc.Controller;
using Rainbow.ViewModels.ClientModules;
using Yunyong.Core;

namespace Rainbow.Platform.Controllers
{
	/// <summary>
    ///     ClientModule Controller
    /// </summary>
    [Display(Name = "ClientModule Controller")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles="SysAdmin")]
    public partial class ClientModuleController: Controller
	{
		/// <summary>
		///     ClientModule Controller构造函数
		/// </summary>
        public ClientModuleController(IClientModuleActionService actionService, IClientModuleQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IClientModuleActionService ActionService { get; }
        private IClientModuleQueryService QueryService { get; }

        /// <summary>
        ///     创建客户端模块
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="创建客户端模块")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync([FromBody]CreateClientModuleVM vm)
        {
            return await ActionService.CreateAsync(vm);
        }
        /// <summary>
        ///     更新客户端模块
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="更新客户端模块")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateClientModuleVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }
        /// <summary>
        ///     查询客户端模块列表（分页）
        /// </summary>
        [Display(Name = "查询客户端模块列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [Authorize]
        [ProducesDefaultResponseType(typeof(PagingList<ClientModuleVM>))]
        public async Task<PagingList<ClientModuleVM>> QueryAsync([FromQuery]QueryClientModuleVM option)
        {
            return await QueryService.QueryAsync(option);
        }
        /// <summary>
        ///     获取客户端模块
        /// </summary>
        [Display(Name = "获取客户端模块")]
        [HttpGet]
        [Route("")]
        [Authorize]
        [ProducesDefaultResponseType(typeof(ClientModuleVM))]
        public async Task < ClientModuleVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取客户端模块列表
        /// </summary>
        [Display(Name = "获取客户端模块列表")]
        [HttpGet]
        [Route("List")]
        [Authorize]
        [ProducesDefaultResponseType(typeof(List<ClientModuleVM>))]
        public async Task < List<ClientModuleVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }


        /// <summary>
        ///     删除客户端模块
        /// </summary>
        [Display(Name="删除客户端模块")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteClientModuleVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}