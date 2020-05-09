using System;
using System.Threading.Tasks;

using Rainbow.ViewModels.AttachmentImages;

namespace Rainbow.Services.AttachmentImages
{
    public interface IImageService
    {
        Task<AttachmentImageVM> GetAsync(Guid imageId);
    }
}
