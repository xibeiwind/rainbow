using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Configs;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class UserActionService : ServiceBase, IUserActionService
    {
        private PictureSettings PictureSettings { get; }
        private IHostingEnvironment Env { get; }

        public UserActionService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus,
            PictureSettings pictureSettings,
            IHostingEnvironment env)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            PictureSettings = pictureSettings;
            Env = env;
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

                await this.UpdateUserAvatarAsync(new UpdateUserAvatarVM() {Id = vm.Id, File = vm.File});

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

        public async Task<AsyncTaskResult> UpdateUserAvatarAsync(UpdateUserAvatarVM vm)
        {
            var imageTypeDic =
                new Dictionary<string, string>
                {
                    {"image/bmp", "bmp"},
                    {"image/gif", "gif"},
                    {"image/x-icon", "ico"},
                    {"image/jpeg", "jpg"},
                    {"image/png", "png"}
                };

            var dirPath = Path.Combine(Env.WebRootPath, PictureSettings.PictureTypePathDir[PictureType.Avatar]);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var fileName = $"{vm.Id}.{imageTypeDic[vm.File.ContentType]}";

            var filePath = Path.Combine(dirPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.File.CopyToAsync(stream);

                using (var conn = GetConnection())
                {
                    await conn.UpdateAsync<UserInfo>(a => a.Id == vm.Id, new
                    {
                        AvatarUrl = $"/{PictureSettings.PictureTypePathDir[PictureType.Avatar]}/{fileName}"
                    });
                    return AsyncTaskResult.Success();
                }
            }
        }
    }
}