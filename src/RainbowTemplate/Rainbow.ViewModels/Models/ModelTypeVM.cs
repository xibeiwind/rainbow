using System.Collections.Generic;

namespace Rainbow.ViewModels.Models
{
    public class ModelTypeVM
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Asssembly { get; set; }
        public string DisplayName { get; set; }

        public List<FieldVM> Fields { get; set; }
    }
}