using System.Collections.Generic;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Users
{
    public class CustomerServiceVM : VMBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}