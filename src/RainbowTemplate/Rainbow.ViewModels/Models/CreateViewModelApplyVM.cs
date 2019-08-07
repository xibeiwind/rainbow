using System.Collections.Generic;
using Rainbow.Common.Enums;

namespace Rainbow.ViewModels.Models
{
    public class CreateViewModelApplyVM
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public VMType Type { get; set; }
        public string ActionName { get; set; }
        public List<string> Fields { get; set; }
    }
}