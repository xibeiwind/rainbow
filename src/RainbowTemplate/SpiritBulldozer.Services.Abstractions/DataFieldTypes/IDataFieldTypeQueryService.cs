using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SpiritBulldozer.ViewModels.DataFieldTypes;
using Yunyong.Core;

namespace SpiritBulldozer.Services.DataFieldTypes
{
    public interface IDataFieldTypeQueryService
    {

        /// <summary>
        ///     获取显示DataFieldType
        /// </summary>
        [Display(Name = "获取显示DataFieldType")]
        Task < DataFieldTypeVM > GetAsync(Guid id);

        /// <summary>
        ///     获取显示DataFieldType列表
        /// </summary>
        [Display(Name = "获取显示DataFieldType列表")]
        Task < List<DataFieldTypeVM> > GetListAsync();

        /// <summary>
        ///     查询DataFieldType列表（分页）
        /// </summary>
        [Display(Name = "查询DataFieldType列表（分页）")]
        Task<PagingList<DataFieldTypeVM>> QueryAsync(QueryDataFieldTypeVM option);

    }
}
