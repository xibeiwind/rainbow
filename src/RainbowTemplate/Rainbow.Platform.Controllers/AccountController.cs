using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            LoginResultVM result = await Service.PasswordLoginWithToken(vm);
            return Ok();
        }


        /// <summary>
        ///     获取用户信息
        /// </summary>
        [HttpGet]
        [Route("UserInfo")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(UserVM))]
        public async Task<UserVM> GetUserAsync()
        {
            return await Service.GetUserAsync(this.GetUserId());
        }

    }
}