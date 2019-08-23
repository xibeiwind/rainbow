using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Common.Enums;
using Rainbow.Services.Users;
using Rainbow.ViewModels.CustomerServices;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerServiceAccountController : Controller
    {
        public CustomerServiceAccountController(ICustomerServiceManageService service)
        {
            Service = service;
        }

        private ICustomerServiceManageService Service { get; }

        /// <summary>
        ///     客服登陆
        /// </summary>
        [Display(Name = "客服登陆")]
        [HttpPost]
        [Route("Login")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<LoginResultVM>))]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var result = await Service.Login(vm);
            if (result.Status == AsyncTaskStatus.Success)
                return Ok(result);
            return Unauthorized(result);
        }

        /// <summary>
        ///     获取客服信息
        /// </summary>
        [Display(Name = "获取客服信息")]
        [HttpGet]
        [Route("CustomerService")]
        [Authorize(Roles = nameof(UserRoleType.CustomerService))]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<CustomerServiceVM>))]
        public async Task<IActionResult> GetCustomerService()
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                var result = await Service.GetCustomerService(userId.Value);
                return Ok(result);
            }

            return null;
        }

        private Guid? GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(a => a.Type == "jti");
            if (Guid.TryParse(claim?.Value, out var userId))
                return userId;
            return default;
        }

        /// <summary>
        ///     退出登陆
        /// </summary>
        [Display(Name = "退出登陆")]
        [HttpPost]
        [Route("Logout")]
        [Authorize(Roles = nameof(UserRoleType.CustomerService))]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<bool>))]
        public async Task<IActionResult> Logout()
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                var result = await Service.Logout(userId.Value);
                if (result.Status == AsyncTaskStatus.Success)
                    return Ok(result);
                return BadRequest(result);
            }

            return BadRequest(AsyncTaskResult.Failed<bool>("账号未登录"));
        }

        [HttpGet]
        [Route("IsLogin")]
        [ProducesDefaultResponseType(typeof(bool))]
        public bool IsLogin()
        {
            return User.Identity?.IsAuthenticated ?? false;
        }
    }
}