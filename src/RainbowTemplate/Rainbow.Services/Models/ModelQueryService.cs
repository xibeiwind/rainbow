using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Rainbow.ViewModels.Models;
using Yunyong.Core;

namespace Rainbow.Services.Models
{
    public class ModelQueryService : IModelQueryService
    {
        public IEnumerable<ModelTypeVM> GetModelTypes()
        {
            

            var types
                = Assembly.Load($"Rainbow.Models").GetTypes().Where(a => !a.IsAbstract && a.IsSubclassOf(typeof(Entity)));

            foreach (var type in types) yield return GetModelTypeVM(type);
        }

        private ModelTypeVM GetModelTypeVM(Type type)
        {
            return new ModelTypeVM
            {
                Name = type.Name,
                FullName = type.FullName,
                Asssembly = type.Assembly.FullName,
                DisplayName = type.GetCustomAttribute<DisplayAttribute>()?.Name ?? type.Name,
                Fields = GetModelTypeFields(type).ToList()
            };
            //throw new NotImplementedException();
        }

        private IEnumerable<FieldVM> GetModelTypeFields(Type type)
        {
            var items = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

            foreach (var item in items)
                yield return new FieldVM
                {
                    Name = item.Name,
                    DisplayName = item.GetCustomAttribute<DisplayAttribute>()?.Name ?? item.Name,
                    Type = item.PropertyType.Name
                };
        }
    }
}