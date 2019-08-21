using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Utils
{
    public class SendSmsRequestVM
    {
        public string Phone { get; set; }
        public TplType CodeType { get; set; }
    }
}