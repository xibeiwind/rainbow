using System;

namespace Rainbow.ViewModels.Users
{
    public class LoginResultVM
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Guid? UserId { get; set; }
    }
}