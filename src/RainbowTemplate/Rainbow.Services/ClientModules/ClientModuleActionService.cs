using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.ClientModules;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.ClientModules
{
    public class ClientModuleActionService : ServiceBase, IClientModuleActionService
    {
        public ClientModuleActionService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus
        )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     创建客户端模块
        /// </summary>
        [Display(Name = "创建客户端模块")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync(CreateClientModuleVM vm)
        {
            await using var conn = GetConnection();
            var entity = EntityFactory.Create<ClientModule, CreateClientModuleVM>(vm);
            // todo:
            await conn.CreateAsync(entity);


            return AsyncTaskResult.Success(entity.Id);
        }


        /// <summary>
        ///     更新客户端模块
        /// </summary>
        [Display(Name = "更新客户端模块")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateClientModuleVM vm)
        {
            await using var conn = GetConnection();
            // todo:
            await conn.UpdateAsync<ClientModule>(a => a.Id == vm.Id, vm);
            return AsyncTaskResult.Success(vm.Id);
        }

        /// <summary>
        ///     删除客户端模块
        /// </summary>
        [Display(Name = "删除客户端模块")]
        public async Task<AsyncTaskResult> DeleteAsync(DeleteClientModuleVM vm)
        {
            await using var conn = GetConnection();
            await conn.DeleteAsync<ClientModule>(a => a.Id == vm.Id);
            return AsyncTaskResult.Success();
        }
    }
}