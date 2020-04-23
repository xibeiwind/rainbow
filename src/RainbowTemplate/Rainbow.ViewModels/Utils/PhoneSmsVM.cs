using System;

using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Utils
{
    public class PhoneSmsVM
    {
        public string Phone { get; set; }
        public string Code { get; set; }
        public TplType CodeType { get; set; }

        public DateTime CreateOn { get; set; }
        /* public bool SendIsSuccess { get; set; }*/
    }
}