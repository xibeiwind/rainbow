using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Utils
{
    public class SendSmsRequestVM
    {
        public string Phone { get; set; }
        public TplType CodeType { get; set; }
    }
}
