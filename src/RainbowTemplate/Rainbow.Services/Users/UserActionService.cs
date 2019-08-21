using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class UserActionService : ServiceBase, IUserActionService
    {
        public UserActionService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     创建User
        /// </summary>
        [Display(Name = "创建User")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync(CreateUserVM vm)
        {
            using (var conn = GetConnection())
            {
                var entity = EntityFactory.Create<UserInfo, CreateUserVM>(vm);
                // todo:
                await conn.CreateAsync(entity);
                return AsyncTaskResult.Success(entity.Id);
            }
        }

        /// <summary>
        ///     更新User
        /// </summary>
        [Display(Name = "更新User")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateUserVM vm)
        {
            using (var conn = GetConnection())
            {
                // todo:
                await conn.UpdateAsync<UserInfo>(a => a.Id == vm.Id, vm);
                return AsyncTaskResult.Success(vm.Id);
            }
        }

        /// <summary>
        ///     删除User
        /// </summary>
        [Display(Name = "删除User")]
        public async Task<AsyncTaskResult> DeleteAsync(DeleteUserVM vm)
        {
            using (var conn = GetConnection())
            {
                await conn.DeleteAsync<UserInfo>(a => a.Id == vm.Id);
                return AsyncTaskResult.Success();
            }
        }
    }
}