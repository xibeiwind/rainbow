using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.ViewModels.Services
{
    public class ServiceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Folder { get; set; }
        public List<ServiceActionVM> Actions { get; set; }
    }

    public class UpdateServiceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Folder { get; set; }
        public List<ServiceActionVM> Actions { get; set; }
    }

    public class QueryServiceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Folder { get; set; }
    }

    public class ServiceActionVM
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public string Arguments { get; set; }
    }
}
