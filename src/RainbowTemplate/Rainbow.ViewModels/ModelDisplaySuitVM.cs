using System.Collections.Generic;

namespace Rainbow.ViewModels
{
    public class ModelDisplaySuitVM
    {
        public string ModelName { get; set; }
        public string DisplayName { get; set; }
        public List<ViewModelDisplayVM> ViewModels { get; set; }
    }
}