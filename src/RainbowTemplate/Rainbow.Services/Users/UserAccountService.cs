using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Events;
using Rainbow.Models;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class UserAccountService:ServiceBase, IUserAccountService
    {
        public UserAccountService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus) 
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        public async Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<User>(a=>a.Phone == vm.Phone))
                {
                    return AsyncTaskResult.Failed<UserVM>("电话已被使用");
                }
                else
                {
                    var user = EntityFactory.Create<User,RegisterUserVM>(vm);
                    await conn.CreateAsync(user);

                    return AsyncTaskResult.Success(await conn.FirstOrDefaultAsync<User, UserVM>(a => a.Id == user.Id));
                }
            }
        }

        public async Task<LoginResultVM> SmsLoginAsync(SmsLoginVM vm)
        {
            var response = await EventBus.RequestAsync<SmsCodeVerifyRequest, SmsCodeVerifyResponse>(new SmsCodeVerifyRequest());
            if (response.IsSuccess)
            {
                return new LoginResultVM
                {
                    UserId = response.UserId,
                    IsSuccess = true,
                };
            }
            else
            {
                return new LoginResultVM()
                {
                    IsSuccess = false,
                    Message = response.ResultDesc
                };
            }
        }

        public async Task<AsyncTaskResult> SendLoginSmsAsync(SendLoginSmsVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<User>(a=>a.Phone == vm.Phone && a.IsActive))
                {
                    var response = await EventBus.RequestAsync<SendSmsCodeRequest, SendSmsCodeResponse>(new SendSmsCodeRequest
                    {
                        Phone = vm.Phone,
                        Type = TplType.LoginSMS
                    });
                    if (response.IsSuccess)
                    {
                        return AsyncTaskResult.Success();
                    }
                    else
                    {
                        return AsyncTaskResult.Failed(response.ResultDesc);
                    }
                }
                else
                {
                    return AsyncTaskResult.Failed("账号不存在或未激活");
                }
            }
        }

        public async Task<UserVM> GetUserAsync(Guid userId)
        {
            using (var conn = GetConnection())
            {
                return await conn.FirstOrDefaultAsync<User, UserVM>(a => a.Id == userId);
            }
        }
    }
}
