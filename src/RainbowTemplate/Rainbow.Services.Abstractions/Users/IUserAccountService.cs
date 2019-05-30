using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Rainbow.ViewModels.Users;
using Yunyong.Core;

namespace Rainbow.Services.Abstractions
{
    public interface IUserAccountService
    {
        /// <summary>
        ///     用户注册
        /// </summary>
        [Display(Name = "用户注册")]
        Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm);

        /// <summary>
        ///     用户登录验证
        /// </summary>
        [Display(Name = "用户登录验证")]
        Task<LoginResultVM> LoginAsync(LoginVM vm);
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
        ///     忘记密码发送短信验证
        /// </summary>
        [Display(Name = "忘记密码发送短信验证")]
        Task<AsyncTaskResult> ForgetPasswordAsync(ForgetPasswordVM vm);

        /// <summary>
        ///     获取用户信息
        /// </summary>
        [Display(Name = "获取用户信息")]
        Task<UserVM> GetUserAsync(Guid userId);

        Task<AsyncTaskTResult<string>> GetUserAvatarUrlAsync(Guid userId);
    }
}