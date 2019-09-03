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
    public partial class DataFieldTypeActionService : ServiceBase, IDataFieldTypeActionService
    {
        public DataFieldTypeActionService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     创建DataFieldType
        /// </summary>
        [Display(Name="创建DataFieldType")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync(CreateDataFieldTypeVM vm)
        {
            using (var conn = GetConnection())
            {
                var entity = EntityFactory.Create<DataFieldType,CreateDataFieldTypeVM>(vm);
                // todo:
                await conn.CreateAsync(entity);
                return AsyncTaskResult.Success(entity.Id);
            }
        }
        /// <summary>
        ///     更新DataFieldType
        /// </summary>
        [Display(Name="更新DataFieldType")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateDataFieldTypeVM vm)
        {
            using (var conn = GetConnection())
            {
                // todo:
                await conn.UpdateAsync<DataFieldType>(a => a.Id == vm.Id, vm);
                return AsyncTaskResult.Success(vm.Id);
            }
        }
        /// <summary>
        ///     删除DataFieldType
        /// </summary>
        [Display(Name="删除DataFieldType")]
        public async Task<AsyncTaskResult> DeleteAsync(DeleteDataFieldTypeVM vm)
        {
            using (var conn = GetConnection())
            {
                await conn.DeleteAsync<DataFieldType>(a => a.Id == vm.Id);
                return AsyncTaskResult.Success();
            }
        }


	}
}