using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Users
{
    public class LoginVM
    {
        public string Phone { get; set; }

        [DataType(DataType.Password)] public string Password { get; set; }
    }
}