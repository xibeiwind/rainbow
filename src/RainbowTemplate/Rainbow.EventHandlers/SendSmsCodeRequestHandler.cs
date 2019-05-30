using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rainbow.Common;
using Rainbow.Events;
using Rainbow.ViewModels.Utils;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.EventHandlers
{
    public class SendSmsCodeRequestHandler :
        EventHandlerBase,
        IAsyncRequestHandler<SendSmsCodeRequest, SendSmsCodeResponse>
    {
        public SendSmsCodeRequestHandler(
            SmsDataAppKeyConfig smsConfig,
            ICacheService<PhoneSmsVM> phoneSmsCodeCacheService,
            List<MessageTplConfig> configList, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            SmsConfig = smsConfig;
            PhoneSmsCodeCacheService = phoneSmsCodeCacheService;
            ConfigList = configList;
        }

        private SmsDataAppKeyConfig SmsConfig { get; }
        private ICacheService<PhoneSmsVM> PhoneSmsCodeCacheService { get; }
        private List<MessageTplConfig> ConfigList { get; }
        private Random Random { get; } = new Random((int)DateTime.Now.Ticks);

        public async Task<SendSmsCodeResponse> Handle(SendSmsCodeRequest request)
        {
            string GetRandomCode()
            {
                return Random.Next(100000, 999999).ToString();
            }

            var phone = request.Phone;
            var tplType = request.Type;
            var smsCode = request.Code ?? GetRandomCode();
            var configList = ConfigList;

            if (configList != null && configList.Any())
            {
                var tplId = configList.FirstOrDefault(a => a.TplType == tplType)?.TplId;

                var targetPhone = phone;
                var sendVerifyCodeKey = SmsConfig.SendSmsAppKey;
                var url = string.Empty;
                var resultDesc = string.Empty;
                var isSuccess = false;

                try
                {
                    if (SmsConfig.SendRealSms)
                    {
                        var tplvalue = HttpUtility.UrlEncode("#code#=" + smsCode, Encoding.UTF8);
                        //将简体汉字转换为Url编码
                        //发送短信
                        url =
                            $"http://v.juhe.cn/sms/send?mobile={targetPhone}&tpl_id={tplId}&tpl_value={tplvalue}&dtype=json&key={sendVerifyCodeKey}";
                        var sendClient = new HttpClient();
                        var sendJson = await sendClient.GetStringAsync(url);

                        if (!string.IsNullOrEmpty(sendJson))
                        {
                            var sendObj = JsonConvert.DeserializeObject<JObject>(sendJson);
                            var count = sendObj["result"]?["count"]?.Value<int>();
                            var fee = sendObj["result"]?["fee"]?.Value<int>();
                            isSuccess = count == 1 && fee == 1;

                            resultDesc = sendObj["reason"]?.Value<string>();
                        }
                        else
                        {
                            resultDesc = "第三发请求返回内容为空";
                        }
                    }
                    else
                    {
                        isSuccess = true;
                    }

                    if (isSuccess)
                    {
                        var cacheKey = tplType + ":" + phone;
                        var cacheObj = new PhoneSmsVM
                        {
                            Phone = phone,
                            Code = smsCode,
                            CodeType = tplType,
                            CreateOn = DateTimeUtil.GetCurrentTime()
                        };
                        PhoneSmsCodeCacheService.Set(cacheKey, cacheObj, TimeSpan.FromMinutes(5));
                    }
                }
                catch (Exception ex)
                {
                    resultDesc =
                        $"发送短信异常错误：Phone：{targetPhone}，URL：{url}，错误内容：{ex.Message + ex.StackTrace + ex.Source}";
                    Console.WriteLine($"{DateTime.Now}：{resultDesc}");
                }

                if (isSuccess)
                    return new SendSmsCodeResponse(request.Id)
                    {
                        Code = smsCode
                    };

                return new SendSmsCodeResponse(request.Id, false, resultDesc);
            }

            return new SendSmsCodeResponse(request.Id, false, "获取短信模板失败");
        }
    }
}