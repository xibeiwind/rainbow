using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Rainbow.ViewModels.Users;

using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public interface IUserQueryService
    {
        /// <summary>
        ///     获取显示User
        /// </summary>
        [Display(Name = "获取显示User")]
        Task<UserVM> GetAsync(Guid id);

        /// <summary>
        ///     获取显示User列表
        /// </summary>
        [Display(Name = "获取显示User列表")]
        Task<List<UserVM>> GetListAsync();

        /// <summary>
        ///     查询User列表（分页）
        /// </summary>
        [Display(Name = "查询User列表（分页）")]
        Task<PagingList<UserVM>> QueryAsync(QueryUserVM option);
    }
}