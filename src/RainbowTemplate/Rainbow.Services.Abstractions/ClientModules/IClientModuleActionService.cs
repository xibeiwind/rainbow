using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Rainbow.ViewModels.ClientModules;

namespace Rainbow.Services.ClientModules
{
    public interface IClientModuleActionService
    {

        /// <summary>
        ///     创建客户端模块
        /// </summary>
        [Display(Name="创建客户端模块")]
        Task<AsyncTaskTResult<Guid>> CreateAsync(CreateClientModuleVM vm);
        /// <summary>
        ///     更新客户端模块
        /// </summary>
        [Display(Name="更新客户端模块")]
        Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateClientModuleVM vm);
        /// <summary>
        ///     删除客户端模块
        /// </summary>
        [Display(Name="删除客户端模块")]
        Task<AsyncTaskResult> DeleteAsync(DeleteClientModuleVM vm);
    }
}
