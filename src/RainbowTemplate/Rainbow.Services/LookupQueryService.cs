using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.ViewModels;
using Yunyong.Core;
using Yunyong.Core.ViewModels;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services
{
    public class LookupQueryService:ServiceBase, ILookupQueryService
    {
        public LookupQueryService(
            ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus) 
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        public async Task<AsyncTaskTResult<IEnumerable<LookupResultVM>>> Query(LookupAttribute attr, string filter)
        {
            using (var conn = GetConnection())
            {
                if (string.IsNullOrEmpty(filter))
                {
                    var result = await conn.QueryAsync<LookupResultVM>(
                        $@"
select Id, {attr.DisplayField} as Name,{attr.ValueField} as Value 
from {attr.Type.Name} 
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
        }

        public AsyncTaskTResult<LookupResultVM> Query(LookupQueryVM vm)
        {
            
            return null;
        }
    }
}
