using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Services.Abstractions.Utils;
using Rainbow.ViewModels.Utils;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.Services.Utils
{
    public class SmsQueryService : ServiceBase, ISmsQueryService
    {
        private ICacheService<PhoneSmsVM> PhoneSmsCacheService { get; }
        private ICacheService<VerifySmsSuccessVM> VerifySmsCacheService { get; }
        private ICacheService<VerfyCodeNumLimitVM> VerifyCodeNumLimitService { get; }

        public SmsQueryService(ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus,
            ICacheService<PhoneSmsVM> phoneSmsCacheService,
        ICacheService<VerifySmsSuccessVM> verifySmsCacheService,
            ICacheService<VerfyCodeNumLimitVM> verifyCodeNumLimitService)
            : base(connectionSettings,
                connectionFactory,
                loggerFactory,
                eventBus)
        {
            PhoneSmsCacheService = phoneSmsCacheService;
            VerifySmsCacheService = verifySmsCacheService;
            VerifyCodeNumLimitService = verifyCodeNumLimitService;
        }

        public async Task<VerifyingSmsCodeResultVM> VerifyingSmsCode(VerifyingSmsCodeRequestVM vm) //string phone, string code, TplType codeType)
        {
            var redisKey = vm.CodeType + ":" + vm.Phone;
            var token = Guid.NewGuid();
            //限制次数
            var lockTime = 2; //锁定时间
            var errorNum = 10; //最大错误次数锁定改手机号验证验证码次数
            int num;
            var newNum = 0;
            var codeNumLimit = VerifyCodeNumLimitService.GetOrDefault<VerfyCodeNumLimitVM>(redisKey);
            if (codeNumLimit != null)
            {
                num = codeNumLimit.ErrorNum;
                newNum = num++;
                if (num > errorNum && codeNumLimit.IsLocked)
                {
                    //return new SmsCodeVerifyResponse(request.Id, false, "手机验证码验证次数过多");
                    return new VerifyingSmsCodeResultVM { Message = "手机验证码验证次数过多", State = false };
                }

            }
            var item = PhoneSmsCacheService.GetOrDefault<PhoneSmsVM>(redisKey);
            if (item != null && (vm.Code ?? string.Empty).Equals(item.Code))
            {
                VerifySmsCacheService.Set(redisKey, new VerifySmsSuccessVM
                {
                    CodeType = vm.CodeType,
                    Phone = vm.Phone,
                    Token = token,
                    Id = Guid.NewGuid(),
                    CreateOn = DateTimeUtil.GetCurrentTime(),
                    IsEnable = true
                }, TimeSpan.FromMinutes(2));

                return new VerifyingSmsCodeResultVM { Token = token, State = true };
            }

            var isLocked = newNum > errorNum;
            var spanTime = newNum > errorNum ? TimeSpan.FromMinutes(lockTime) : TimeSpan.FromMinutes(5);
            VerifyCodeNumLimitService.Set(redisKey, new VerfyCodeNumLimitVM
            {
                IsLocked = isLocked,
                ErrorNum = newNum,
                Phone = vm.Phone
            }, spanTime);

            return new VerifyingSmsCodeResultVM { Message = "短信验证失败", State = false };
        }
    }
}
