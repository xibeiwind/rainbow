using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.Utils;

namespace Rainbow.Services.Abstractions.Utils
{
    public interface ISmsActionService
    {
        Task<SendSmsResultVM> SendSms(string phone, TplType tplType);
    }
}
