using System;
using Yunyong.EventBus;

namespace Rainbow.Events
{
    public class SmsCodeVerifyResponse : EventResponse
    {
        public SmsCodeVerifyResponse(Guid requestId, bool isSuccess = true, string resultDesc = "") : base(requestId, isSuccess, resultDesc)
        {
        }

        public Guid ResponseToken { get; set; }
        public Guid? UserId { get; set; }
    }
}