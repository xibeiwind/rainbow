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

using Rainbow.ViewModels.Users;

namespace Rainbow.Services.Users
{
    public class UserQueryService : ServiceBase, IUserQueryService
    {
        public UserQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取显示User
        /// </summary>
        [Display(Name = "获取显示User")]
        public async Task < UserVM> GetAsync(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<User, UserVM>(a => a.Id == id);
            }
        }

        /// <summary>
        ///     获取显示User列表
        /// </summary>
        [Display(Name = "获取显示User列表")]
        public async Task <List<UserVM>> GetListAsync()
        {
            using (var conn = GetConnection())
            {
                return await conn.AllAsync<User, UserVM>();
            }
        }

        /// <summary>
        ///     查询User列表（分页）
        /// </summary>
        [Display(Name = "查询User列表（分页）")]
        public async Task<PagingList<UserVM>> QueryAsync(QueryUserVM option)
        {
            using (var conn = GetConnection())
            {
                return await conn.PagingListAsync<User, UserVM>(option);
            }
        }


	}
}