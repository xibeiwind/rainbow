using Rainbow.ViewModels.AttachmentImages;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.AttachmentImages
{
    public partial interface IManageAttachmentImageActionService
    {
        Task<AsyncTaskTResult<string>> UploadPictureAsync(UploadPictureRequestVM vm);
        Task<AsyncTaskTResult<UploadPictureAdvResultVM>> UploadPictureAdvAsync(UploadPictureAdvRequestVM vm);

        Task<AsyncTaskResult> UploadPictureFileToCloudAsync(UploadPictureFileToCloudRequestVM vm);
    }
}