using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Users;
using Rainbow.ViewModels.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Yunyong.Mvc.Controller" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public AccountController(IUserAccountService service)
        {
            Service = service;
        }

        private IUserAccountService Service { get; }

        /// <summary>
        ///     用户注册
        /// </summary>
        [Display(Name = "用户注册")]
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> Register([FromBody] RegisterUserVM vm)
        {
            return await Service.RegisterAsync(vm);
        }
        /// <summary>
        ///     登录
        /// </summary>
        [Display(Name = "登录")]
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(LoginResultVM))]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            return Ok(await Service.PasswordLoginWithToken(vm));
        }

        /// <summary>
        ///     退出登录
        /// </summary>
        [Display(Name = "退出登录")]
        [HttpPost]
        [Route("Logout")]
        [Authorize]
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
        private Guid? GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(a => a.Type == "jti");
            if (Guid.TryParse(claim?.Value, out var userId))
                return userId;
            return default;
        }

        private Guid? GetSignId()
        {
            var claim = User.Claims.FirstOrDefault(a => a.Type == "signId");
            if (Guid.TryParse(claim?.Value, out var signId))
                return signId;
            return default;
        }

        /// <summary>
        ///     获取用户信息
        /// </summary>
        [Display(Name = "获取用户信息")]
        [HttpGet]
        [Route("UserInfo")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(UserProfileVM))]
        public async Task<IActionResult> GetUserAsync()
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                return Ok(await Service.GetUserAsync(userId.Value));

            }

            return NotFound();
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
            var userId = GetUserId();
            var signId = GetSignId();
            if (userId.HasValue && signId.HasValue)
            {
                return Ok(await Service.IsLogin(userId.Value, signId.Value));

            }

            return Ok(false);
            //Service.IsLogin()
            //return Ok(User.Identity?.IsAuthenticated ?? false);
        }
        /// <summary>
        ///    用户是否具有某角色
        /// </summary>
        [Display(Name = "用户是否具有某角色")]
        [HttpGet]
        [Authorize]
        [Route("UserInRole/{roleName}")]

        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<bool>))]
        public async Task<IActionResult> UserInRole(string roleName)
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                return Ok(await Service.UserInRole(userId.Value, roleName));
            }

            return BadRequest(AsyncTaskResult.Failed<bool>("failed"));
        }

    }
}