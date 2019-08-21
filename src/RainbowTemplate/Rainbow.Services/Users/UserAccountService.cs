using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Events;
using Rainbow.Models;
using Rainbow.Services.Utils;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class UserAccountService : ServiceBase, IUserAccountService
    {
        public UserAccountService(ConnectionSettings connectionSettings, SecurityUtil securityUtil,
            IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            SecurityUtil = securityUtil;
        }

        private SecurityUtil SecurityUtil { get; }

        public async Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<UserInfo>(a => a.Phone == vm.Phone))
                    return AsyncTaskResult.Failed<UserVM>("电话已被使用");

                var user = EntityFactory.Create<UserInfo, RegisterUserVM>(vm);
                await conn.CreateAsync(user);

                return AsyncTaskResult.Success(await conn.FirstOrDefaultAsync<UserInfo, UserVM>(a => a.Id == user.Id));
            }
        }

        public async Task<LoginResultVM> SmsLoginAsync(SmsLoginVM vm)
        {
            var response =
                await EventBus.RequestAsync<SmsCodeVerifyRequest, SmsCodeVerifyResponse>(new SmsCodeVerifyRequest());
            if (response.IsSuccess)
                return new LoginResultVM
                {
                    UserId = response.UserId,
                    IsSuccess = true
                };
            return new LoginResultVM
            {
                IsSuccess = false,
                Message = response.ResultDesc
            };
        }

        public async Task<AsyncTaskResult> SendLoginSmsAsync(SendLoginSmsVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<UserInfo>(a => a.Phone == vm.Phone && a.IsActive))
                {
                    var response = await EventBus.RequestAsync<SendSmsCodeRequest, SendSmsCodeResponse>(
                        new SendSmsCodeRequest
                        {
                            Phone = vm.Phone,
                            Type = TplType.LoginSMS
                        });
                    if (response.IsSuccess)
                        return AsyncTaskResult.Success();
                    return AsyncTaskResult.Failed(response.ResultDesc);
                }

                return AsyncTaskResult.Failed("账号不存在或未激活");
            }
        }

        public async Task<UserVM> GetUserAsync(Guid userId)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<UserInfo, UserVM>(a => a.Id == userId);
            }
        }

        public async Task<LoginResultVM> PasswordLoginWithToken(LoginVM vm)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Phone == vm.Phone);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        if (string.IsNullOrEmpty(user.PasswordHash))
                            return new LoginResultVM
                            {
                                IsSuccess = false,
                                Message = "账号不允许使用密码登陆"
                            };

                        var pwdHash = SecurityUtil.Encoding(vm.Password);
                        if (string.Equals(user.PasswordHash, pwdHash, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var claims = new[]
                            {
                                new Claim("jti", user.Id.ToString(), ClaimValueTypes.String),
                                new Claim(ClaimTypes.MobilePhone, user.Phone),
                                new Claim(ClaimTypes.Name, user.Name)
                            };
                        }
                    }
                    else
                    {
                        return new LoginResultVM
                        {
                            IsSuccess = false,
                            Message = "账号未激活"
                        };
                    }
                }
                else
                {
                    return new LoginResultVM
                    {
                        IsSuccess = false,
                        Message = "账号不存在"
                    };
                }
            }

            return null;
        }
    }
}