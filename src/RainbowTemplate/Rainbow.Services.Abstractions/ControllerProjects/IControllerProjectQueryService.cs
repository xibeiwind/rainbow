using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rainbow.ViewModels.ControllerProjects;
using Yunyong.Core;

namespace Rainbow.Services.ControllerProjects
{
    public interface IControllerProjectQueryService
    {

        /// <summary>
        ///     获取Controller项目
        /// </summary>
        [Display(Name = "获取Controller项目")]
        Task < ControllerProjectVM > GetAsync(Guid id);

        /// <summary>
        ///     获取Controller项目列表
        /// </summary>
        [Display(Name = "获取Controller项目列表")]
        Task < List<ControllerProjectVM> > GetListAsync();
        /// <summary>
        ///     查询Controller项目列表（分页）
        /// </summary>
        [Display(Name = "查询Controller项目列表（分页）")]
        Task<PagingList<ControllerProjectVM>> QueryAsync(QueryControllerProjectVM option);
    }
}
