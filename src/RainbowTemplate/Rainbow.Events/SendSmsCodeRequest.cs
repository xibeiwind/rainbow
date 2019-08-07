﻿using System.Collections.Generic;
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
}