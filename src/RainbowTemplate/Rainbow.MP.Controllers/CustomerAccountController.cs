using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.CustomerInfos;
using Rainbow.ViewModels.CustomerInfos;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.MP.Controllers
{
    /// <summary>
    ///     客户微信登录
    /// </summary>
    [Display(Name = "客户微信登录")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerAccountController : Controller
    {
        /// <summary>
        /// </summary>
        public CustomerAccountController(
            ICustomerAccountService service,
            ICustomerInfoQueryService queryService)
        {
            Service = service;
            QueryService = queryService;
        }

        private ICustomerAccountService Service { get; }
        private ICustomerInfoQueryService QueryService { get; }

        /// <summary>
        /// </summary>
        protected override void Controller_BeforeAction()
        {
            base.Controller_BeforeAction();
        }


        /// <summary>
        ///     登录
        /// </summary>
        [Display(Name = "登录")]
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(WechatLoginResultVM))]
        public async Task<IActionResult> Login(WechatLoginVM vm)
        {
            return Ok(await Service.Login(vm));
        }

        /// <summary>
        ///     退出登录
        /// </summary>
        [Display(Name = "退出登录")]
        [HttpPost]
        [Route("Logout")]
        [Authorize]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<bool>))]
        public IActionResult Logout()
        {
            var customerId = this.GetCustomerId();
            if (customerId != Guid.Empty)
            {
                var result = Service.Logout(customerId);
                if (result.Status == AsyncTaskStatus.Success)
                    return Ok(result);
                return BadRequest(result);
            }

            return BadRequest(AsyncTaskResult.Failed<bool>("账号未登录"));
        }

        /// <summary>
        ///     是否已登录
        /// </summary>
        [Display(Name = "是否已登录")]
        [HttpGet]
        [Route("IsLogin")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<IActionResult> IsLogin()
        {
            return Ok(await Service.IsLogin(this.GetCustomerId(), this.GetSignId()));
        }

        /// <summary>
        ///     获取客户信息
        /// </summary>
        [Display(Name = "获取客户信息")]
        [HttpGet]
        [Route("CustomerInfo")]
        [ProducesDefaultResponseType(typeof(CustomerInfoVM))]
        [Authorize]
        public async Task<IActionResult> GetCustomerInfoAsync()
        {
            var customerId = this.GetCustomerId();
            if (customerId != Guid.Empty)
            {
                QueryService.CustomerId = customerId;
                var item = await QueryService.GetAsync();
                return item != null ? (IActionResult)Ok(item) : NotFound();
            }

            return NotFound();
        }
    }
}
