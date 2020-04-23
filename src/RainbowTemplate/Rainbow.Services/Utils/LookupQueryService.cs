using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Dapper;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.ViewModels.Utils;

using Yunyong.Core;
using Yunyong.Core.Attributes;
using Yunyong.EventBus;

namespace Rainbow.Services.Utils
{
    public class LookupQueryService : ServiceBase, ILookupQueryService
    {
        public LookupQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        public async Task<AsyncTaskTResult<IEnumerable<LookupResultVM>>> Query(LookupAttribute attr, string filter)
        {
            await using var conn = GetConnection();
            if (string.IsNullOrEmpty(filter))
            {
                var result = await conn.QueryAsync<LookupResultVM>(
                    $@"
select Id, {attr.DisplayField} as Name,{attr.ValueField} as Value 
from {attr.TypeName} 
order by {attr.DisplayField} limit 10");

                return AsyncTaskResult.Success(result);
            }
            else
            {
                var result = await conn.QueryAsync<LookupResultVM>(
                    $@"
select Id, {attr.DisplayField} as Name,{attr.ValueField} as Value 
from {attr.ValueField} 
where {attr.DisplayField} like @Filter 
order by {attr.DisplayField} limit 10",
                    new {Filter = $"%{filter}%"});

                return AsyncTaskResult.Success(result);
            }
        }

        public async Task<IEnumerable<LookupResultVM>> QueryAsync(LookupQueryVM vm)
        {
            await using var conn = GetConnection();
            try
            {
                if (string.IsNullOrEmpty(vm.Filter))
                {
                    var result = await conn.QueryAsync<LookupResultVM>(
                        $@"
select Id, {vm.DisplayField} as Name,{vm.ValueField} as Value 
from {vm.TypeName} 
order by {vm.DisplayField} limit 10");

                    return result;
                }
                else
                {
                    var result = await conn.QueryAsync<LookupResultVM>(
                        $@"
select Id, {vm.DisplayField} as Name,{vm.ValueField} as Value 
from {vm.TypeName} 
where {vm.DisplayField} like @Filter 
order by {vm.DisplayField} limit 10",
                        new {Filter = $"%{vm.Filter}%"});

                    return result;
                }
            }
            catch (Exception)
            {
                return new List<LookupResultVM>();
            }
        }

        public async Task<LookupResultVM> GetAsync(LookupQueryVM vm)
        {
            await using var conn = GetConnection();
            try
            {
                if (string.IsNullOrEmpty(vm.Filter))
                {
                    return null;
                }

                var sql = $@"
    select Id, {vm.DisplayField} as Name,{vm.ValueField} as Value 
    from {vm.TypeName} 
    where {vm.ValueField} = @Filter 
    order by {vm.DisplayField} limit 1";

                //XDebug.SQL
                return await conn.QueryFirstAsync<LookupResultVM>(sql, new {vm.Filter});
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}