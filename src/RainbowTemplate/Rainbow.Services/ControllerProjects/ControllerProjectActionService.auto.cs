﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Models;
using Rainbow.ViewModels.ControllerProjects;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.ControllerProjects
{
    public class ControllerProjectActionService : ServiceBase, IControllerProjectActionService
    {
        public ControllerProjectActionService(
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }


        /// <summary>
        ///     创建Controller项目
        /// </summary>
        [Display(Name = "创建Controller项目")]
        public async Task<AsyncTaskTResult<Guid>> CreateAsync(CreateControllerProjectVM vm)
        {
            await using var conn = GetConnection();
            var entity = EntityFactory.Create<ControllerProject, CreateControllerProjectVM>(vm);
            // todo:
            await conn.CreateAsync(entity);
            return AsyncTaskResult.Success(entity.Id);
        }

        /// <summary>
        ///     更新Controller项目
        /// </summary>
        [Display(Name = "更新Controller项目")]
        public async Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateControllerProjectVM vm)
        {
            await using var conn = GetConnection();
            // todo:
            await conn.UpdateAsync<ControllerProject>(a => a.Id == vm.Id, vm);
            return AsyncTaskResult.Success(vm.Id);
        }

        /// <summary>
        ///     删除Controller项目
        /// </summary>
        [Display(Name = "删除Controller项目")]
        public async Task<AsyncTaskResult> DeleteAsync(DeleteControllerProjectVM vm)
        {
            await using var conn = GetConnection();
            await conn.DeleteAsync<ControllerProject>(a => a.Id == vm.Id);
            return AsyncTaskResult.Success();
        }
    }
}