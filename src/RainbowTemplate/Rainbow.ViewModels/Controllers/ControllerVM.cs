using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.ViewModels.Controllers
{
    public class ControllerVM
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class ControllerAction
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ReturnType { get; set; }
        public string Argument { get; set; }
    }
}
