using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Rainbow.Services.RoleInfos;
using Controller = Yunyong.Mvc.Controller;
using Rainbow.ViewModels.RoleInfos;
using Yunyong.Core;

namespace Rainbow.Platform.Controllers
{
	/// <summary>
    ///     RoleInfo Controller
    /// </summary>
    [Display(Name = "RoleInfo Controller")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleInfoController: Controller
	{
		/// <summary>
		///     RoleInfo Controller构造函数
		/// </summary>
        public RoleInfoController(IRoleInfoActionService actionService, IRoleInfoQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IRoleInfoActionService ActionService { get; }
        private IRoleInfoQueryService QueryService { get; }


        /// <summary>
        ///     创建角色
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="创建角色")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync([FromBody]CreateRoleInfoVM vm)
        {
            return await ActionService.CreateAsync(vm);
        }

        /// <summary>
        ///     更新角色
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="更新角色")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateRoleInfoVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }

        /// <summary>
        ///     查询角色列表（分页）
        /// </summary>
        [Display(Name = "查询角色列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesDefaultResponseType(typeof(PagingList<RoleInfoVM>))]
        public async Task<PagingList<RoleInfoVM>> QueryAsync([FromQuery]QueryRoleInfoVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     获取角色
        /// </summary>
        [Display(Name = "获取角色")]
        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(RoleInfoVM))]
        public async Task < RoleInfoVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取角色列表
        /// </summary>
        [Display(Name = "获取角色列表")]
        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(List<RoleInfoVM>))]
        public async Task < List<RoleInfoVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }

        /// <summary>
        ///     删除角色
        /// </summary>
        [Display(Name="删除角色")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteRoleInfoVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}