using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Rainbow.Services.DataFieldTypes;
using Controller = Yunyong.Mvc.Controller;
using Rainbow.ViewModels.DataFieldTypes;
using Yunyong.Core;

namespace Rainbow.Platform.Controllers
{
	/// <summary>
    ///     DataFieldType Controller
    /// </summary>
    [Display(Name = "DataFieldType Controller")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles="SysAdmin")]
    public class DataFieldTypeController: Controller
	{
		/// <summary>
		///     DataFieldType Controller构造函数
		/// </summary>
        public DataFieldTypeController(IDataFieldTypeActionService actionService, IDataFieldTypeQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IDataFieldTypeActionService ActionService { get; }
        private IDataFieldTypeQueryService QueryService { get; }


        /// <summary>
        ///     创建DataFieldType
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="创建DataFieldType")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync([FromBody]CreateDataFieldTypeVM vm)
        {
            return await ActionService.CreateAsync(vm);
        }

        /// <summary>
        ///     更新DataFieldType
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name="更新DataFieldType")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateDataFieldTypeVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }

        /// <summary>
        ///     查询DataFieldType列表（分页）
        /// </summary>
        [Display(Name = "查询DataFieldType列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesDefaultResponseType(typeof(PagingList<DataFieldTypeVM>))]
        public async Task<PagingList<DataFieldTypeVM>> QueryAsync([FromQuery]QueryDataFieldTypeVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     获取DataFieldType
        /// </summary>
        [Display(Name = "获取DataFieldType")]
        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(DataFieldTypeVM))]
        public async Task < DataFieldTypeVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取DataFieldType列表
        /// </summary>
        [Display(Name = "获取DataFieldType列表")]
        [HttpGet]
        [Route("List")]
        [ProducesDefaultResponseType(typeof(List<DataFieldTypeVM>))]
        public async Task < List<DataFieldTypeVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }

        /// <summary>
        ///     删除DataFieldType
        /// </summary>
        [Display(Name="删除DataFieldType")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteDataFieldTypeVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}