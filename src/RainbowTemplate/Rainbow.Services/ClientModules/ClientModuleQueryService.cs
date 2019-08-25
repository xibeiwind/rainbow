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

using Rainbow.ViewModels.ClientModules;

namespace Rainbow.Services.ClientModules
{
    public class ClientModuleQueryService : ServiceBase, IClientModuleQueryService
    {
        public ClientModuleQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取客户端模块
        /// </summary>
        [Display(Name = "获取客户端模块")]
        public async Task < ClientModuleVM> GetAsync(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<ClientModule, ClientModuleVM>(a => a.Id == id);
            }
        }
        /// <summary>
        ///     获取客户端模块列表
        /// </summary>
        [Display(Name = "获取客户端模块列表")]
        public async Task <List<ClientModuleVM>> GetListAsync()
        {
            using (var conn = GetConnection())
            {
                return await conn.AllAsync<ClientModule, ClientModuleVM>();
            }
        }


        /// <summary>
        ///     查询客户端模块列表（分页）
        /// </summary>
        [Display(Name = "查询客户端模块列表（分页）")]
        public async Task<PagingList<ClientModuleVM>> QueryAsync(QueryClientModuleVM option)
        {
            using (var conn = GetConnection())
            {
                return await conn.PagingListAsync<ClientModule, ClientModuleVM>(option);
            }
        }

	}
}