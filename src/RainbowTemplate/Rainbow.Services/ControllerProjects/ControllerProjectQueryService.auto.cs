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

using Rainbow.ViewModels.ControllerProjects;

namespace Rainbow.Services.ControllerProjects
{
    public partial class ControllerProjectQueryService : ServiceBase, IControllerProjectQueryService
    {
        public ControllerProjectQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     查询Controller项目列表（分页）
        /// </summary>
        [Display(Name = "查询Controller项目列表（分页）")]
        public async Task<PagingList<ControllerProjectVM>> QueryAsync(QueryControllerProjectVM option)
        {
            using (var conn = GetConnection())
            {
                return await conn.PagingListAsync<ControllerProject, ControllerProjectVM>(option);
            }
        }
        /// <summary>
        ///     获取Controller项目
        /// </summary>
        [Display(Name = "获取Controller项目")]
        public async Task < ControllerProjectVM> GetAsync(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<ControllerProject, ControllerProjectVM>(a => a.Id == id);
            }
        }
        /// <summary>
        ///     获取Controller项目列表
        /// </summary>
        [Display(Name = "获取Controller项目列表")]
        public async Task <List<ControllerProjectVM>> GetListAsync()
        {
            using (var conn = GetConnection())
            {
                return await conn.AllAsync<ControllerProject, ControllerProjectVM>();
            }
        }



	}
}