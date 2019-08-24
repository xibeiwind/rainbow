using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Yunyong.Core;
using Yunyong.EventBus;
using Yunyong.DataExchange;

using Rainbow.ViewModels.RoleInfos;

namespace Rainbow.Services.RoleInfos
{
    public class RoleInfoQueryService : ServiceBase, IRoleInfoQueryService
    {
        public RoleInfoQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取显示角色
        /// </summary>
        [Display(Name = "获取显示角色")]
        public async Task < RoleInfoVM> GetAsync(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<RoleInfo, RoleInfoVM>(a => a.Id == id);
            }
        }
        /// <summary>
        ///     获取显示角色列表
        /// </summary>
        [Display(Name = "获取显示角色列表")]
        public async Task <List<RoleInfoVM>> GetListAsync()
        {
            using (var conn = GetConnection())
            {
                return await conn.AllAsync<RoleInfo, RoleInfoVM>();
            }
        }


        /// <summary>
        ///     查询角色列表（分页）
        /// </summary>
        [Display(Name = "查询角色列表（分页）")]
        public async Task<PagingList<RoleInfoVM>> QueryAsync(QueryRoleInfoVM option)
        {
            using (var conn = GetConnection())
            {
                conn.OpenDebug();
                var result = await conn.PagingListAsync<RoleInfo, RoleInfoVM>(option);


                var tmp = XDebug.SqlWithParams;

                return result;
            }
        }

	}
}