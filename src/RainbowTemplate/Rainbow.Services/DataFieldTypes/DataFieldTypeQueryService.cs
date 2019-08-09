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

using Rainbow.ViewModels.DataFieldTypes;

namespace Rainbow.Services.DataFieldTypes
{
    public class DataFieldTypeQueryService : ServiceBase, IDataFieldTypeQueryService
    {
        public DataFieldTypeQueryService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     获取显示DataFieldType
        /// </summary>
        [Display(Name = "获取显示DataFieldType")]
        public async Task < DataFieldTypeVM> GetAsync(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<DataFieldType, DataFieldTypeVM>(a => a.Id == id);
            }
        }

        /// <summary>
        ///     获取显示DataFieldType列表
        /// </summary>
        [Display(Name = "获取显示DataFieldType列表")]
        public async Task <List<DataFieldTypeVM>> GetListAsync()
        {
            using (var conn = GetConnection())
            {
                return await conn.AllAsync<DataFieldType, DataFieldTypeVM>();
            }
        }

        /// <summary>
        ///     查询DataFieldType列表（分页）
        /// </summary>
        [Display(Name = "查询DataFieldType列表（分页）")]
        public async Task<PagingList<DataFieldTypeVM>> QueryAsync(QueryDataFieldTypeVM option)
        {
            using (var conn = GetConnection())
            {
                return await conn.PagingListAsync<DataFieldType, DataFieldTypeVM>(option);
            }
        }


	}
}