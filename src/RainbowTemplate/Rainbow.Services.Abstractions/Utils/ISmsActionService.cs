using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.Utils;

namespace Rainbow.Services.Utils
{
    public interface ISmsActionService
    {
        Task<SendSmsResultVM> SendSms(string phone, TplType tplType);
    }
}
