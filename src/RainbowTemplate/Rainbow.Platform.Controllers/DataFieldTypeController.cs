using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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

    public class DataFieldTypeController: Controller
	{

        public DataFieldTypeController(IDataFieldTypeActionService actionService, IDataFieldTypeQueryService queryService)
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IDataFieldTypeActionService ActionService { get; }
        private IDataFieldTypeQueryService QueryService { get; }


        /// <summary>
        ///     获取显示DataFieldType
        /// </summary>
        [Display(Name = "获取显示DataFieldType")]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(DataFieldTypeVM), 200)]
        public async Task < DataFieldTypeVM > GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     获取显示DataFieldType列表
        /// </summary>
        [Display(Name = "获取显示DataFieldType列表")]
        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(List<DataFieldTypeVM>), 200)]
        public async Task < List<DataFieldTypeVM> > GetListAsync()
        {
            return await QueryService.GetListAsync();
        }

        /// <summary>
        ///     查询DataFieldType列表（分页）
        /// </summary>
        [Display(Name = "查询DataFieldType列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesResponseType(typeof(PagingList<DataFieldTypeVM>), 200)]
        public async Task<PagingList<DataFieldTypeVM>> QueryAsync([FromQuery]QueryDataFieldTypeVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     创建DataFieldType
        /// </summary>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(AsyncTaskTResult<Guid>), 200)]
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
        [ProducesResponseType(typeof(AsyncTaskTResult<Guid>), 200)]
        [Display(Name="更新DataFieldType")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync([FromBody]UpdateDataFieldTypeVM vm)
        {
            return await ActionService.UpdateAsync(vm);
        }

        /// <summary>
        ///     删除DataFieldType
        /// </summary>
        [Display(Name="删除DataFieldType")]
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]DeleteDataFieldTypeVM vm)
        {
            return await ActionService.DeleteAsync(vm);
        }

	}
}