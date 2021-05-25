using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Configs;
using Rainbow.Events.Utils;
using Rainbow.Models;
using Rainbow.ViewModels.AttachmentImages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.AttachmentImages
{
    public class ManageAttachmentImageActionService : ServiceBase, IManageAttachmentImageActionService
    {
        private readonly Dictionary<string, string> _imageTypeDic =
            new Dictionary<string, string>
            {
                {"image/bmp", "bmp"},
                {"image/gif", "gif"},
                {"image/x-icon", "ico"},
                {"image/jpeg", "jpg"},
                {"image/png", "png"}
            };

        /// <summary>
        /// </summary>
        public ManageAttachmentImageActionService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            //IHostingEnvironment env,
            WebPathConfig config,
            IEventBus eventBus,
            PictureSettings settings
        ) : base(connectionSettings, connectionFactory, loggerFactory,
            eventBus)
        {
            //Env = env;
            Config = config;
            Settings = settings;
        }

        //private IHostingEnvironment Env { get; }
        private WebPathConfig Config { get; }
        public PictureSettings Settings { get; }

        #region Implementation of IManageAttachmentPictureActionService

        public async Task<AsyncTaskTResult<string>> UploadPictureAsync(UploadPictureRequestVM vm)
        {
            var item = EntityFactory.Create<AttachmentImage>();
            if (vm.TargetId == Guid.Empty) vm.TargetId = Guid.NewGuid();

            item.TargetId = vm.TargetId;
            var fileName = $"{item.Id}.{_imageTypeDic[vm.File.ContentType]}";
            var filePath = Path.Combine(Config.WebRootPath, Settings.AttachmentPictureDir, fileName);
            try
            {
                await using (var conn = GetConnection())
                {
                    await conn.DeleteAsync<AttachmentImage>(a => a.TargetId == vm.TargetId);

                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.File.CopyToAsync(stream);
                    }

                    item.FileName = $"{Settings.AttachmentPictureDir}/{fileName}";

                    await conn.CreateAsync(item);
                }

                EventBus.Publish(new AttachmentImageUploadEvent
                {
                    ImageId = item.Id,
                    FileName = item.FileName,
                    FilePath = filePath
                });

                return AsyncTaskResult.Success<string>(item.FileName);
            }
            catch (Exception ex)
            {
                return AsyncTaskResult.Failed<string>(ex.Message);
            }
        }

        public async Task<AsyncTaskTResult<UploadPictureAdvResultVM>> UploadPictureAdvAsync(
            UploadPictureAdvRequestVM vm
        )
        {
            var item = EntityFactory.Create<AttachmentImage>();
            item.TargetId = Guid.Empty;
            var fileName = $"{item.Id}.{_imageTypeDic[vm.Upload.ContentType]}";
            var filePath = Path.Combine(Config.WebRootPath, Settings.AttachmentPictureDir, fileName);
            try
            {
                await using var conn = GetConnection();
                await using var stream = new FileStream(filePath, FileMode.Create);
                await vm.Upload.CopyToAsync(stream);

                item.FileName = $"{Settings.AttachmentPictureDir}/{fileName}";

                await conn.CreateAsync(item);

                EventBus.Publish(new AttachmentImageUploadEvent
                {
                    ImageId = item.Id,
                    FileName = item.FileName,
                    FilePath = filePath
                });

                return AsyncTaskResult.Success(new UploadPictureAdvResultVM
                {
                    Id = item.Id,
                    FileName = fileName,
                    ImageUrl = item.FileName
                });
            }
            catch (Exception ex)
            {
                return AsyncTaskResult.Failed<UploadPictureAdvResultVM>(ex.Message);
            }
        }

        public async Task<AsyncTaskResult> UploadPictureFileToCloudAsync(UploadPictureFileToCloudRequestVM vm)
        {
            await using var conn = GetConnection();
            foreach (var pictureId in vm.ImageIds)
            {
                var entity = await conn.FirstOrDefaultAsync<AttachmentImage>(a => a.Id == pictureId);

                //var dirPath = Path.Combine(Env.WebRootPath, Settings.AttachmentPictureDir);

                var fileName = entity.FileName;
                var filePath = Path.Combine(Config.WebRootPath, fileName.Replace("/", "\\"));
                EventBus.Publish(new AttachmentImageUploadEvent
                {
                    ImageId = pictureId,
                    FileName = fileName,
                    FilePath = filePath,
                    ForceUpload = true
                });
            }

            return AsyncTaskResult.Success();
        }

        #endregion
    }
}