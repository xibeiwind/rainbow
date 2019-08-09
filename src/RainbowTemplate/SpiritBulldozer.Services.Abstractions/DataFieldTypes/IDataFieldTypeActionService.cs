using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using SpiritBulldozer.ViewModels.DataFieldTypes;

namespace SpiritBulldozer.Services.DataFieldTypes
{
    public interface IDataFieldTypeActionService
    {

        /// <summary>
        ///     创建DataFieldType
        /// </summary>
        [Display(Name="创建DataFieldType")]
        Task<AsyncTaskTResult<Guid>> CreateAsync(CreateDataFieldTypeVM vm);

        /// <summary>
        ///     更新DataFieldType
        /// </summary>
        [Display(Name="更新DataFieldType")]
        Task<AsyncTaskTResult<Guid>> UpdateAsync(UpdateDataFieldTypeVM vm);

        /// <summary>
        ///     删除DataFieldType
        /// </summary>
        [Display(Name="删除DataFieldType")]
        Task<AsyncTaskResult> DeleteAsync(DeleteDataFieldTypeVM vm);

    }
}
