using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Common.Enums;
using Rainbow.Services.Users;
using Rainbow.ViewModels.Users;
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
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> RegisterAsync([FromBody] RegisterUserVM vm)
        {
            return await Service.RegisterAsync(vm);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(LoginResultVM))]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            return Ok(await Service.PasswordLoginWithToken(vm));
        }

        /// <summary>
        ///     退出登陆
        /// </summary>
        [Display(Name = "退出登陆")]
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

        [HttpGet]
        [Route("IsLogin")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult IsLogin()
        {
            var userId = GetUserId();
            var signId = GetSignId();
            if (userId.HasValue && signId.HasValue)
            {
                return Ok(Service.IsLogin(userId.Value, signId.Value));

            }

            return Ok(false);
            //Service.IsLogin()
            //return Ok(User.Identity?.IsAuthenticated ?? false);
        }



    }
}