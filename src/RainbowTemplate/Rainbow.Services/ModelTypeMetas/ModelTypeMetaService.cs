using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.ModelTypeMetas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.ModelTypeMetas
{
    public class ModelTypeMetaService : ServiceBase, IModelTypeMetaService
    {
        public ModelTypeMetaService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        public async Task<AsyncTaskTResult<Guid>> CreateUpdateAsync(CreateUpdateModelTypeMetaVM vm)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            await using var conn = GetConnection();
            var item = await conn.FirstOrDefaultAsync<ModelTypeMeta>(a => a.TypeName == vm.TypeName);
            if (item != null)
            {



                item.JSON = JsonConvert.SerializeObject(vm.ViewMetas, settings);
                await conn.UpdateAsync<ModelTypeMeta>(a => a.Id == item.Id, item);
                return AsyncTaskResult.Success(item.Id);
            }
            else
            {
                item = new ModelTypeMeta
                {
                    TypeName = vm.TypeName,
                    JSON = JsonConvert.SerializeObject(vm.ViewMetas, settings),
                };
                await conn.CreateAsync(item);
                return AsyncTaskResult.Success(item.Id);
            }
        }

        public async Task<AsyncTaskTResult<ModelTypeMetaVM>> GetModelTypeMetaAsync(string name)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            await using var conn = GetConnection();
            var item = await conn.FirstOrDefaultAsync<ModelTypeMeta>(a => a.TypeName == name);
            if (item != null)
            {
                var viewMetas = JsonConvert.DeserializeObject<List<ModelViewTypeMetaVM>>(item.JSON);
                var vm = new ModelTypeMetaVM
                {
                    Id = item.Id,
                    TypeName = item.TypeName,
                    ViewMetas = viewMetas,
                    CanCreate = viewMetas.Exists(a => a is CreateModelViewTypeMetaVM),
                    CanEdit = viewMetas.Exists(a => a is EditModelViewTypeMetaVM),
                };

                return AsyncTaskResult.Success(vm);
            }
            else
            {
                return AsyncTaskResult.Failed<ModelTypeMetaVM>("not find");
            }
        }
    }
}
