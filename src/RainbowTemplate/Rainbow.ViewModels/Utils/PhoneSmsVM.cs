﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Yunyong.EventBus;

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
