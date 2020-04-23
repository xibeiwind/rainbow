using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.RoleInfos;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.RoleInfos
{
    public class RoleInfoQueryService : ServiceBase, IRoleInfoQueryService
    {
        public RoleInfoQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     查询角色列表（分页）
        /// </summary>
        [Display(Name = "查询角色列表（分页）")]
        public async Task<PagingList<RoleInfoVM>> QueryAsync(QueryRoleInfoVM option)
        {
            await using var conn = GetConnection();
            return await conn.PagingListAsync<RoleInfo, RoleInfoVM>(option);
        }

        /// <summary>
        ///     获取角色
        /// </summary>
        [Display(Name = "获取角色")]
        public async Task<RoleInfoVM> GetAsync(Guid id)
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<RoleInfo, RoleInfoVM>(a => a.Id == id);
        }

        /// <summary>
        ///     获取角色列表
        /// </summary>
        [Display(Name = "获取角色列表")]
        public async Task<List<RoleInfoVM>> GetListAsync()
        {
            await using var conn = GetConnection();
            return await conn.AllAsync<RoleInfo, RoleInfoVM>();
        }
    }
}