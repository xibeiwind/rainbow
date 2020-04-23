using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.RoleInfos;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.RoleInfos
{
    public class RoleInfoActionService : ServiceBase, IRoleInfoActionService
    {
        public RoleInfoActionService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     创建角色
        /// </summary>
        [Display(Name = "创建角色")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync(CreateRoleInfoVM vm)
        {
            await using var conn = GetConnection();
            var entity = EntityFactory.Create<RoleInfo, CreateRoleInfoVM>(vm);
            // todo:
            await conn.CreateAsync(entity);
            return AsyncTaskResult.Success(entity.Id);
        }

        /// <summary>
        ///     更新角色
        /// </summary>
        [Display(Name = "更新角色")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateRoleInfoVM vm)
        {
            await using var conn = GetConnection();
            // todo:
            await conn.UpdateAsync<RoleInfo>(a => a.Id == vm.Id, vm);
            return AsyncTaskResult.Success(vm.Id);
        }

        /// <summary>
        ///     删除角色
        /// </summary>
        [Display(Name = "删除角色")]
        public async Task<AsyncTaskResult> DeleteAsync(DeleteRoleInfoVM vm)
        {
            await using var conn = GetConnection();
            await conn.DeleteAsync<RoleInfo>(a => a.Id == vm.Id);
            return AsyncTaskResult.Success();
        }
    }
}