using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Rainbow.Common.Enums;
using Rainbow.Services.Users;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.Mvc;
using Controller = Yunyong.Mvc.Controller;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Platform.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class CustomerServiceAccountController : Controller
    {
        private ICustomerServiceManageService Service { get; }

        public CustomerServiceAccountController(ICustomerServiceManageService service)
        {
            Service = service;
        }

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
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
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
            else
            {
                return default;
            }

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
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
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
