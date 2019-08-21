using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Rainbow.ViewModels.Users;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public interface IUserActionService
    {
        /// <summary>
        ///     创建User
        /// </summary>
        [Display(Name = "创建User")]
        Task<AsyncTaskTResult<Guid>> CreateAsync(CreateUserVM vm);

        /// <summary>
        ///     更新User
        /// </summary>
        [Display(Name = "更新User")]
        Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateUserVM vm);

        /// <summary>
        ///     删除User
        /// </summary>
        [Display(Name = "删除User")]
        Task<AsyncTaskResult> DeleteAsync(DeleteUserVM vm);
    }
}