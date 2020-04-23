using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Rainbow.ViewModels.RoleInfos;

using Yunyong.Core;

namespace Rainbow.Services.RoleInfos
{
    public interface IRoleInfoQueryService
    {
        /// <summary>
        ///     获取角色
        /// </summary>
        [Display(Name = "获取角色")]
        Task<RoleInfoVM> GetAsync(Guid id);

        /// <summary>
        ///     获取角色列表
        /// </summary>
        [Display(Name = "获取角色列表")]
        Task<List<RoleInfoVM>> GetListAsync();

        /// <summary>
        ///     查询角色列表（分页）
        /// </summary>
        [Display(Name = "查询角色列表（分页）")]
        Task<PagingList<RoleInfoVM>> QueryAsync(QueryRoleInfoVM option);
    }
}