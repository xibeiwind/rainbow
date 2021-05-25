using Rainbow.ViewModels.ControllerProjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.ControllerProjects
{
    public interface IControllerProjectActionService
    {
        /// <summary>
        ///     创建Controller项目
        /// </summary>
        [Display(Name = "创建Controller项目")]
        Task<AsyncTaskTResult<Guid>> CreateAsync(CreateControllerProjectVM vm);

        /// <summary>
        ///     更新Controller项目
        /// </summary>
        [Display(Name = "更新Controller项目")]
        Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateControllerProjectVM vm);

        /// <summary>
        ///     删除Controller项目
        /// </summary>
        [Display(Name = "删除Controller项目")]
        Task<AsyncTaskResult> DeleteAsync(DeleteControllerProjectVM vm);
    }
}