using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    public class RegisterUserVM : CreateVM
    {

    }

    public class LoginVM
    {
        public string Phone { get; set; }
        public string Password { get; set; }

    }

    public class SmsLoginVM
    {
        public string Phone { get; set; }
        public string SmsCode { get; set; }
    }

    public class SendLoginSmsVM
    {
        public string Phone { get; set; }
    }
    public class ForgetPasswordVM
    {
        public string Phone { get; set; }

    }

    public class LoginResultVM
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Guid? UserId { get; set; }

    }

    public class UserVM:VMBase
    {
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }
}
