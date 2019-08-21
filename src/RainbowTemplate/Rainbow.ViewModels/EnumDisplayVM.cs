using System.Collections.Generic;

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