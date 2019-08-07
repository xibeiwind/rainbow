using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Rainbow.ViewModels.Users;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public interface IUserAccountService
    {
        /// <summary>
        ///     用户注册
        /// </summary>
        [Display(Name = "用户注册")]
        Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm);

        /// <summary>
        ///     手机号码短信登录
        /// </summary>
        [Display(Name = "手机号码短信登录")]
        Task<LoginResultVM> SmsLoginAsync(SmsLoginVM vm);
        /// <summary>
        ///     发送登录短信
        /// </summary>
        [Display(Name = "发送登录短信")]
        Task<AsyncTaskResult> SendLoginSmsAsync(SendLoginSmsVM vm);

        /// <summary>
        ///     获取用户信息
        /// </summary>
        [Display(Name = "获取用户信息")]
        Task<UserVM> GetUserAsync(Guid userId);
    }
}