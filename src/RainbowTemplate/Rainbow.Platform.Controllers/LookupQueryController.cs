using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Utils;
using Rainbow.ViewModels.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    ///     Lookup搜索
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LookupQueryController : Controller
    {
        /// <summary>
        /// </summary>
        /// <param name="queryService"></param>
        public LookupQueryController(ILookupQueryService queryService)
        {
            QueryService = queryService;
        }

        private ILookupQueryService QueryService { get; }

        /// <summary>
        ///     Lookup查询
        /// </summary>
        [Display(Name = "Lookup查询")]
        [HttpGet]
        [Route("Query")]
        [ProducesDefaultResponseType(typeof(List<LookupResultVM>))]
        public async Task<IActionResult> QueryAsync([FromQuery] LookupQueryVM vm)
        {
            return Ok(await QueryService.QueryAsync(vm));
        }

        /// <summary>
        ///     Lookup获取
        /// </summary>
        [Display(Name = "Lookup获取")]
        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(LookupResultVM))]
        public async Task<IActionResult> GetAsync([FromQuery] LookupQueryVM vm)
        {
            var item = await QueryService.GetAsync(vm);
            if (item != null)
                return Ok(item);
            return NotFound();
        }
    }
}