using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rainbow.ViewModels;

namespace Rainbow.Services
{
    public class TypeQueryService : ITypeQueryService
    {
        HashSet<string> typeData = new HashSet<string>();

        public TypeQueryService()
        {
            var types = "string double int float decimal bool byte Guid".Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var type in types)
            {
                typeData.Add(type);
            }

            var items = typeof(ViewModelDisplayVM).Assembly.GetTypes().Where(a => !a.IsAbstract).Select(a => a.Name);
            foreach (var item in items)
            {
                typeData.Add(item);
            }

        }
        public IEnumerable<string> Query(string keyword)
        {
            return typeData.Where(a => a.Contains(keyword)).Take(10);
        }
    }
}
