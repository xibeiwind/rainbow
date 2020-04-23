using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.Users;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class UserQueryService : ServiceBase, IUserQueryService
    {
        public UserQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取显示User
        /// </summary>
        [Display(Name = "获取显示User")]
        public async Task<UserVM> GetAsync(Guid id)
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<UserInfo, UserVM>(a => a.Id == id);
        }

        /// <summary>
        ///     获取显示User列表
        /// </summary>
        [Display(Name = "获取显示User列表")]
        public async Task<List<UserVM>> GetListAsync()
        {
            await using var conn = GetConnection();
            return await conn.AllAsync<UserInfo, UserVM>();
        }

        /// <summary>
        ///     查询User列表（分页）
        /// </summary>
        [Display(Name = "查询User列表（分页）")]
        public async Task<PagingList<UserVM>> QueryAsync(QueryUserVM option)
        {
            await using var conn = GetConnection();
            return await conn.PagingListAsync<UserInfo, UserVM>(option);
        }
    }
}