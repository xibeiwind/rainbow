using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Abstractions;
using Rainbow.Services.Abstractions.Users;
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
    
    }
}