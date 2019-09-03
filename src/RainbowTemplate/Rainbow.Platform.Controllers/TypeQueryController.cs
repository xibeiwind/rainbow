using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    ///     类型查询服务
    /// </summary>
    [Display(Name = "类型查询服务")]
    public class TypeQueryController:Controller
    {
        private ITypeQueryService Service { get; }

        /// <summary>
        ///     构造函数
        /// </summary>
        public TypeQueryController(ITypeQueryService service)
        {
            Service = service;
        }
        /// <summary>
        ///     类型查询
        /// </summary>
        [Display(Name = "类型查询")]
        [HttpGet]
        [Route("Query/{keyword}")]
        [ProducesDefaultResponseType(typeof(IEnumerable<string>))]
        public IEnumerable<string> Query(string keyword)
        {
            return Service.Query(keyword);
        }
        
    }
}
