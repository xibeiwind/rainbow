using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.ControllerProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.ControllerProjects
{
    public class ControllerProjectQueryService : ServiceBase, IControllerProjectQueryService
    {
        public ControllerProjectQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     查询Controller项目列表（分页）
        /// </summary>
        [Display(Name = "查询Controller项目列表（分页）")]
        public async Task<PagingList<ControllerProjectVM>> QueryAsync(QueryControllerProjectVM option)
        {
            await using var conn = GetConnection();
            return await conn.PagingListAsync<ControllerProject, ControllerProjectVM>(option);
        }

        /// <summary>
        ///     获取Controller项目
        /// </summary>
        [Display(Name = "获取Controller项目")]
        public async Task<ControllerProjectVM> GetAsync(Guid id)
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<ControllerProject, ControllerProjectVM>(a => a.Id == id);
        }

        /// <summary>
        ///     获取Controller项目列表
        /// </summary>
        [Display(Name = "获取Controller项目列表")]
        public async Task<List<ControllerProjectVM>> GetListAsync()
        {
            await using var conn = GetConnection();
            return await conn.AllAsync<ControllerProject, ControllerProjectVM>();
        }
    }
}