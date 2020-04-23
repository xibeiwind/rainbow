using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Rainbow.ViewModels.ClientModules;

using Yunyong.Core;

namespace Rainbow.Services.ClientModules
{
    public interface IClientModuleQueryService
    {
        /// <summary>
        ///     获取客户端模块
        /// </summary>
        [Display(Name = "获取客户端模块")]
        Task<ClientModuleVM> GetAsync(Guid id);

        /// <summary>
        ///     获取客户端模块列表
        /// </summary>
        [Display(Name = "获取客户端模块列表")]
        Task<List<ClientModuleVM>> GetListAsync();

        /// <summary>
        ///     查询客户端模块列表（分页）
        /// </summary>
        [Display(Name = "查询客户端模块列表（分页）")]
        Task<PagingList<ClientModuleVM>> QueryAsync(QueryClientModuleVM option);
    }
}