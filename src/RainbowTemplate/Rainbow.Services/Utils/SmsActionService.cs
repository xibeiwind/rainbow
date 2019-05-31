using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Events;
using Rainbow.Models;
using Rainbow.Services.Abstractions.Utils;
using Rainbow.ViewModels.Utils;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Utils
{
    public class SmsActionService : ServiceBase, ISmsActionService
    {
        public SmsActionService(ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus,
            SmsVerifyLockSetting lockSetting,
            ICacheService<PhoneSmsVM> phoneSmsCacheService,
            ICacheService<VerifySmsSuccessVM> verifySmsCacheService,
            ICacheService<VerfyCodeNumLimitVM> verifyCodeNumLimitService)
            : base(connectionSettings,
                connectionFactory,
                loggerFactory,
                eventBus)
        {
            LockSetting = lockSetting;
            PhoneSmsCacheService = phoneSmsCacheService;
            VerifySmsCacheService = verifySmsCacheService;
            VerifyCodeNumLimitService = verifyCodeNumLimitService;
        }

        private SmsVerifyLockSetting LockSetting { get; }
        private ICacheService<PhoneSmsVM> PhoneSmsCacheService { get; }
        private ICacheService<VerifySmsSuccessVM> VerifySmsCacheService { get; }
        private ICacheService<VerfyCodeNumLimitVM> VerifyCodeNumLimitService { get; }
        private Random Random { get; } = new Random((int)DateTime.Now.Ticks);

        public async Task<SendSmsResultVM> SendSms(string phone, TplType tplType)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<User>(a => a.Phone == phone);
                if (user?.IsActive!=true)
                {
                    return new SendSmsResultVM
                    {
                        State = false,
                        Message = "账号不存在或未激活，不能登陆！"
                    };
                }
            }
            var response = EventBus.Request<SendSmsCodeRequest, SendSmsCodeResponse>(new SendSmsCodeRequest
            {
                Phone = phone,
                Type = tplType,
                Code = GetRandomCode()
            });
            if (response?.IsSuccess == true)
                return new SendSmsResultVM
                {
                    State = true,
                    SmsCode = response.Code
                };

            return new SendSmsResultVM
            {
                State = false,
                Message = response?.ResultDesc
            };
        }
        private string GetRandomCode()
        {
            return Random.Next(100000, 999999).ToString();
        }
    }
}