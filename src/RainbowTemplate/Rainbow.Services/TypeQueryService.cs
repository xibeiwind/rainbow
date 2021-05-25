using Rainbow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rainbow.Services
{
    public class TypeQueryService : ITypeQueryService
    {
        private readonly HashSet<string> _typeData = new HashSet<string>();

        public TypeQueryService()
        {
            var types = "string double int float decimal bool byte Guid".Split(new[] {" "},
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var type in types) _typeData.Add(type);

            var items = typeof(ViewModelDisplayVM).Assembly.GetTypes().Where(a => !a.IsAbstract).Select(a => a.Name);
            foreach (var item in items) _typeData.Add(item);
        }

        public IEnumerable<string> Query(string keyword)
        {
            return _typeData.Where(a => a.Contains(keyword)).Take(10);
        }
    }
}