using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.CustomerInfos;
using System;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.CustomerInfos
{
    public class CustomerInfoQueryService : ServiceBase, ICustomerInfoQueryService
    {
        /// <summary>
        /// </summary>
        public CustomerInfoQueryService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        #region Implementation of ICustomerInfoQueryService

        public Guid CustomerId { get; set; }

        public async Task<CustomerInfoVM> GetAsync()
        {
            await using var conn = GetConnection();
            return await conn.FirstOrDefaultAsync<CustomerInfo, CustomerInfoVM>(a => a.Id == CustomerId);
        }

        #endregion
    }
}