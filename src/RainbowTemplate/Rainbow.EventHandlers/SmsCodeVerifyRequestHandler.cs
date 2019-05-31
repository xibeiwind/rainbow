using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Events;
using Rainbow.Models;
using Rainbow.ViewModels.Utils;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.EventHandlers
{
    public class SmsCodeVerifyRequestHandler:
        EventHandlerBase,
        IAsyncRequestHandler<SmsCodeVerifyRequest, SmsCodeVerifyResponse>
    {
        private ICacheService<PhoneSmsVM> PhoneSmsCacheService { get; }
        private ICacheService<VerifySmsSuccessVM> VerifySmsCacheService { get; }
        private ICacheService<VerfyCodeNumLimitVM> VerifyCodeNumLimitService { get; }
        private SmsVerifyLockSetting LockSetting { get; }

        public SmsCodeVerifyRequestHandler(
            ICacheService<PhoneSmsVM> phoneSmsCacheService,
            ICacheService<VerifySmsSuccessVM> verifySmsCacheService,
            ICacheService<VerfyCodeNumLimitVM> verifyCodeNumLimitService,
            SmsVerifyLockSetting lockSetting,
            ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            PhoneSmsCacheService = phoneSmsCacheService;
            VerifySmsCacheService = verifySmsCacheService;
            VerifyCodeNumLimitService = verifyCodeNumLimitService;
            LockSetting = lockSetting;
        }

        public async Task<SmsCodeVerifyResponse> Handle(SmsCodeVerifyRequest request)
        {
            var redisKey = $"{request.CodeType}:{request.Phone}";
            var codeNumLimit = VerifyCodeNumLimitService.GetOrDefault<VerfyCodeNumLimitVM>(redisKey);
            if (codeNumLimit != null && LockSetting.IsVerifyOverdose(codeNumLimit))
            {
                //return new VerifyingSmsCodeResultVM { Message = "手机验证码验证次数过多", State = false };

                return new SmsCodeVerifyResponse(request.Id, false, "手机验证码验证次数过多");
            }
            var item = PhoneSmsCacheService.GetOrDefault<PhoneSmsVM>(redisKey);
            if (item != null && request.SmsCode.Equals(item.Code, StringComparison.OrdinalIgnoreCase))
            {
                var token = Guid.NewGuid();
                VerifySmsCacheService.Set(redisKey, new VerifySmsSuccessVM
                {
                    CodeType = request.CodeType,
                    Phone = request.Phone,
                    Token = token,
                    Id = GuidUtil.NewSequentialId(),
                    CreateOn = DateTimeUtil.GetCurrentTime(),
                    IsEnable = true
                }, TimeSpan.FromMinutes(2));
                VerifyCodeNumLimitService.Remove(redisKey);

                return new SmsCodeVerifyResponse(request.Id);
            }

            var errorTimes = (codeNumLimit?.ErrorNum ?? 0) + 1;

            var isLocked = errorTimes > LockSetting.LockTriggerErrorTimes;
            var spanTime = isLocked ? TimeSpan.FromMinutes(LockSetting.LockMinutes) : TimeSpan.FromMinutes(5);
            VerifyCodeNumLimitService.Set(redisKey, new VerfyCodeNumLimitVM
            {
                IsLocked = isLocked,
                ErrorNum = errorTimes,
                Phone = request.Phone
            }, spanTime);
            return new SmsCodeVerifyResponse(request.Id, false, "短信验证失败");
        }
    }
}
