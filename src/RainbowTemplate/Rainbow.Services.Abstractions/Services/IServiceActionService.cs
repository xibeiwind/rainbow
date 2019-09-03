using Rainbow.ViewModels.Services;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;

namespace Rainbow.Services.Services
{
    public interface IServiceActionService
    {
        /// <summary>
        ///     更新服务
        /// </summary>
        [Display(Name = "更新服务")]
        AsyncTaskTResult<ServiceVM> UpdateAsync(UpdateServiceVM vm);
        /// <summary>
        ///     删除服务
        /// </summary>
        [Display(Name = "删除服务")]
        AsyncTaskTResult<bool> DeleteAsync(string serviceName);
    }
}