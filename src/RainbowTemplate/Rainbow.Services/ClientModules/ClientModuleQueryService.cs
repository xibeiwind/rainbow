using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.ClientModules;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.ClientModules
{
    public class ClientModuleQueryService : ServiceBase, IClientModuleQueryService
    {
        public ClientModuleQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取客户端模块
        /// </summary>
        [Display(Name = "获取客户端模块")]
        public async Task<ClientModuleVM> GetAsync(Guid id)
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<ClientModule, ClientModuleVM>(a => a.Id == id);
        }

        /// <summary>
        ///     获取客户端模块列表
        /// </summary>
        [Display(Name = "获取客户端模块列表")]
        public async Task<List<ClientModuleVM>> GetListAsync()
        {
            await using var conn = GetConnection();
            return await conn.AllAsync<ClientModule, ClientModuleVM>();
        }


        /// <summary>
        ///     查询客户端模块列表（分页）
        /// </summary>
        [Display(Name = "查询客户端模块列表（分页）")]
        public async Task<PagingList<ClientModuleVM>> QueryAsync(QueryClientModuleVM option)
        {
            await using var conn = GetConnection();
            return await conn.PagingListAsync<ClientModule, ClientModuleVM>(option);
        }
    }
}