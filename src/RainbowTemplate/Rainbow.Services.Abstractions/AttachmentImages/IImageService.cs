using Rainbow.ViewModels.AttachmentImages;
using System;
using System.Threading.Tasks;

namespace Rainbow.Services.AttachmentImages
{
    public interface IImageService
    {
        Task<AttachmentImageVM> GetAsync(Guid imageId);
    }
}
