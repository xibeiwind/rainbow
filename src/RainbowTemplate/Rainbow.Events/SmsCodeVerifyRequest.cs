using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Yunyong.EventBus;

namespace Rainbow.Events
{
    public class SmsCodeVerifyRequest : EventRequest
    {
        public string Phone { get; set; }
        public TplType CodeType { get; set; }
        public string SmsCode { get; set; }
    }
}
