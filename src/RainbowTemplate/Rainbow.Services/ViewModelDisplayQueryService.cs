using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.ViewModels;
using Yunyong.Core;

namespace Rainbow.Services
{
    public class ViewModelDisplayQueryService : IViewModelDisplayQueryService
    {
        public ViewModelDisplayQueryService()
        {
            string FieldType(PropertyInfo property)
            {
                string GetFieldType(Type type)
                {
                    if (type.IsEnum) return type.Name;

                    var dic = new Dictionary<string, List<Type>>
                    {
                        {
                            "text", new List<Type>
                            {
                                typeof(string), typeof(Guid)
                            }
                        },
                        {
                            "number", new List<Type>
                            {
                                typeof(int),
                                typeof(double),
                                typeof(float),
                                typeof(decimal),
                                typeof(byte)
                            }
                        },
                        {
                            "checkbox",
                            new List<Type>
                            {
                                typeof(bool)
                            }
                        }
                    };
                    KeyValuePair<string, List<Type>>? tmp = dic.FirstOrDefault(a => a.Value.Contains(type));

                    return tmp?.Key ?? "text";
                }

                if (!property.PropertyType.IsClass)
                    return GetFieldType(property.PropertyType);
                if (property.PropertyType.IsSubclassOf(typeof(Nullable<>)))
                    return GetFieldType(property.PropertyType.GetGenericArguments().FirstOrDefault());
                return "text";
            }

            ModelTypeDic = Assembly.Load("Rainbow.Models").GetTypes()
                .Where(a => a.IsSubclassOf(typeof(Entity)) && !a.IsAbstract)
                .ToDictionary(b => b.Name);

            var items = typeof(ViewModelDisplayVM).Assembly.GetTypes().Where(a => !a.IsAbstract).Select(a =>
            {
                var typeName = a.Name;
                var typeDisplayName = a.GetCustomAttribute<DisplayAttribute>()?.Name ?? typeName;
                var props = a.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default |
                                            BindingFlags.DeclaredOnly);
                return new ViewModelDisplayVM
                {
                    Name = typeName,
                    ModelName = a.GetCustomAttribute<BindModelAttribute>()?.ModelName,
                    DisplayName = typeDisplayName,
                    Type = a.GetCustomAttribute<BindModelAttribute>()?.Type ?? VMType.None,
                    Fields = props.Select(b =>
                    {
                        var propName = b.Name;
                        var propDisplayName = b.GetCustomAttribute<DisplayAttribute>()?.Name ?? propName;
                        var lookup = b.GetCustomAttribute<LookupAttribute>();

                        return new FieldDisplayVM
                        {
                            Name = propName,
                            DisplayName = propDisplayName,
                            FieldType = FieldType(b),
                            IsEnum = b.PropertyType.IsEnum,
                            DataType = GetDataType(b),
                            Lookup = lookup != null
                                ? new LookupSettingVM
                                {
                                    VMType = lookup.Type.Name,
                                    DisplayField = lookup.DisplayField,
                                    ValueField = lookup.ValueField
                                }
                                : null,
                            IsNullable = b.PropertyType.IsSubclassOf(typeof(Nullable<>))
                                         || b.PropertyType.IsClass
                        };
                    }).ToList()
                };
            });

            ViewModelDisplayDic = items.ToDictionary(a => a.Name);
        }

        private Dictionary<string, Type> ModelTypeDic { get; }

        private Dictionary<string, ViewModelDisplayVM> ViewModelDisplayDic { get; }

        /// <summary>
        ///     获取ViewMoel 展示信息
        /// </summary>
        [Display(Name = "获取ViewMoel 展示信息")]
        public async Task<AsyncTaskTResult<ViewModelDisplayVM>> GetVMDisplay(string name)
        {
            if (ViewModelDisplayDic.TryGetValue(name, out var value))
                return AsyncTaskResult.Success(value);
            return AsyncTaskResult.Failed<ViewModelDisplayVM>("not find");
        }

        public async Task<AsyncTaskTResult<ModelDisplaySuitVM>> GetModelVMDisplays(string modelName)
        {
            if (ModelTypeDic.ContainsKey(modelName))
            {
                var type = ModelTypeDic[modelName];
                return new AsyncTaskTResult<ModelDisplaySuitVM>(new ModelDisplaySuitVM
                {
                    ModelName = type.Name,
                    DisplayName = type.GetCustomAttribute<DisplayAttribute>()?.Name ?? type.Name,
                    ViewModels = ViewModelDisplayDic.Values
                        .Where(a => string.Equals(a.ModelName, modelName, StringComparison.InvariantCultureIgnoreCase))
                        .OrderBy(a => a.Type).ToList()
                });
            }

            return AsyncTaskResult.Failed<ModelDisplaySuitVM>($"Model:[{modelName}] 未找到");
        }

        private DataType GetDataType(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<DataTypeAttribute>();
            if (attr != null) return attr.DataType;
            if (property.PropertyType.IsSubclassOf(typeof(Nullable<>)))
            {
                var type = property.PropertyType.GetGenericArguments().FirstOrDefault();
            }

            return DataType.Text;
        }
    }
}