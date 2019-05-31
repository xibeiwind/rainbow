using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.EventBus;

namespace Rainbow.Events
{
    public class SendSmsCodeResponse : EventResponse
    {
        public SendSmsCodeResponse(Guid requestId, bool isSuccess = true, string resultDesc = "") : base(requestId, isSuccess, resultDesc)
        {
        }
        [Display(Name = "验证码")]
        public string Code { get; set; }
    }
}