using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Abstractions;
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
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> RegisterAsync([FromBody] RegisterUserVM vm)
        {
            return await Service.RegisterAsync(vm);
        }

        /// <summary>
        ///     登录
        /// </summary>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResultVM), 200)]
        public async Task<LoginResultVM> LoginAsync([FromBody] LoginVM vm)
        {
            return await Service.LoginAsync(vm);
        }

        /// <summary>
        ///     忘记密码，发送验证码
        /// </summary>
        [HttpGet]
        [Route("ForgetPassword")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> ForgetPasswordAsync([FromQuery] ForgetPasswordVM vm)
        {
            return await Service.ForgetPasswordAsync(vm);
        }

        /// <summary>
        ///     获取用户信息
        /// </summary>
        [HttpGet]
        [Route("UserInfo")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserVM), 200)]
        public async Task<UserVM> GetUserAsync()
        {
            return await Service.GetUserAsync(this.GetUserId());
        }

        /// <summary>
        ///     获取用户头像Url
        /// </summary>
        [HttpGet]
        [Route("UserAvatar/{userId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<AsyncTaskTResult<string>> UserAvatar(Guid userId)
        {
            return AsyncTaskResult.Success<string>("http://qcloud.dpfile.com/pc/e8OoMdPk5qBpAfBjwfwF40kly4WCkN1W3WuOTSVCiN5oiBktvQII5H1EHxzyMYbmTYGVDmosZWTLal1WbWRW3A.jpg");

            return await Service.GetUserAvatarUrlAsync(userId);
        }
    }
}