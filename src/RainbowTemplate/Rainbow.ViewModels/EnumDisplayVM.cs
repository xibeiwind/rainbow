using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.ViewModels
{
    public class EnumDisplayVM
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<EnumFieldDisplayVM> Fields { get; set; }

    }
}
