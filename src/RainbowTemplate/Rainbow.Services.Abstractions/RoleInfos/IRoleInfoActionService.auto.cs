using Rainbow.ViewModels.RoleInfos;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.RoleInfos
{
    public interface IRoleInfoActionService
    {
        /// <summary>
        ///     创建角色
        /// </summary>
        [Display(Name = "创建角色")]
        Task<AsyncTaskTResult<Guid>> CreateAsync(CreateRoleInfoVM vm);

        /// <summary>
        ///     更新角色
        /// </summary>
        [Display(Name = "更新角色")]
        Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateRoleInfoVM vm);

        /// <summary>
        ///     删除角色
        /// </summary>
        [Display(Name = "删除角色")]
        Task<AsyncTaskResult> DeleteAsync(DeleteRoleInfoVM vm);
    }
}