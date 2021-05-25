using Rainbow.Common.Enums;
using Rainbow.ViewModels.Utils;
using System.Threading.Tasks;

namespace Rainbow.Services.Utils
{
    public interface ISmsActionService
    {
        Task<SendSmsResultVM> SendSms(string phone, TplType tplType);
    }
}