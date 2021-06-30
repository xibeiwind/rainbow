using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Users;
using Rainbow.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    ///     User Controller
    /// </summary>
    [Display(Name = "User Controller")]
    [ApiController]
    [Route("api/[controller]")]

    public class UserController: Controller
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionService"></param>
        /// <param name="queryService"></param>
        public UserController(IUserActionService actionService, IUserQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IUserActionService ActionService { get; }
        private IUserQueryService QueryService { get; }


        /// <summary>
        ///     获取显示User
        /// </summary>
        [Display(Name = "获取显示User")]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(UserVM), 200)]
        public async Task < UserVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取显示User列表
        /// </summary>
        [Display(Name = "获取显示User列表")]
        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public async Task < List<UserVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }

        /// <summary>
        ///     查询User列表（分页）
        /// </summary>
        [Display(Name = "查询User列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesResponseType(typeof(PagingList<UserVM>), 200)]
        public async Task<PagingList<UserVM>> QueryAsync([FromQuery]QueryUserVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     创建User
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(AsyncTaskTResult<Guid>), 200)]
        [Display(Name="创建User")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync([FromBody]CreateUserVM vm)
        {
            return await ActionService.CreateAsync(vm);
        }

        /// <summary>
        ///     更新User
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(AsyncTaskTResult<Guid>), 200)]
        [Display(Name="更新User")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateUserVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }

        /// <summary>
        ///     删除User
        /// </summary>
        [Display(Name="删除User")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteUserVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}