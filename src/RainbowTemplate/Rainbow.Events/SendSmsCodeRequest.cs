using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Yunyong.EventBus;

namespace Rainbow.Events
{
    public class SendSmsCodeRequest : EventRequest
    {
        public string Phone { get; set; }
        public TplType Type { get; set; }
        public string Code { get; set; }
    }

    public class SendSmsCodeResponse : EventResponse
    {
        public SendSmsCodeResponse(Guid requestId, bool isSuccess = true, string resultDesc = "") : base(requestId, isSuccess, resultDesc)
        {
        }
        [Display(Name = "验证码")]
        public string Code { get; set; }
    }
}
