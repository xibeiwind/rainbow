using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rainbow.ViewModels.Services;

namespace Rainbow.Services.Services
{
    public interface IServiceQueryService
    {
        /// <summary>
        ///     获取服务列表
        /// </summary>
        [Display(Name = "获取服务列表")]
        IEnumerable<ServiceVM> ListAsync();

        /// <summary>
        ///     查询服务
        /// </summary>
        [Display(Name = "查询服务")]
        IEnumerable<ServiceVM> QueryAsync(QueryServiceVM vm);
    }
}