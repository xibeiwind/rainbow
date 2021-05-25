using Microsoft.AspNetCore.Http;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.ViewModels;
using Rainbow.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.Core.Attributes;

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

                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return GetFieldType(property.PropertyType.GetGenericArguments().FirstOrDefault());
                if (!property.PropertyType.IsClass) return GetFieldType(property.PropertyType);

                return "text";
            }

            InputControlType GetInputControlType(PropertyInfo property)
            {
                if (property.PropertyType == typeof(IFormFile)) return InputControlType.FileSelect;
                if (property.GetCustomAttribute<LookupAttribute>() != null) return InputControlType.Select;
                if (property.PropertyType.IsEnum) return InputControlType.Select;

                InputControlType GetControlType(Type type)
                {
                    var dic = new Dictionary<InputControlType, List<Type>>
                    {
                        {
                            InputControlType.Input, new List<Type>
                            {
                                typeof(string), typeof(Guid),
                                typeof(int),
                                typeof(double),
                                typeof(float),
                                typeof(decimal),
                                typeof(byte)
                            }
                        },
                        {
                            InputControlType.Checkbox, new List<Type>
                            {
                                typeof(bool)
                            }
                        }
                    };
                    KeyValuePair<InputControlType, List<Type>>? tmp = dic.FirstOrDefault(a => a.Value.Contains(type));

                    return tmp?.Key ?? InputControlType.Input;
                }

                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return GetControlType(property.PropertyType.GetGenericArguments().FirstOrDefault());
                if (!property.PropertyType.IsClass) return GetControlType(property.PropertyType);
                return InputControlType.Input;
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
                            ControlType = GetInputControlType(b),
                            IsEnum = PropertyIsEnum(b), // b.PropertyType.IsEnum,
                            DataType = GetDataType(b),
                            Lookup = lookup != null
                                ? new LookupSettingVM
                                {
                                    TypeName = lookup.TypeName,
                                    DisplayField = lookup.DisplayField,
                                    ValueField = lookup.ValueField
                                }
                                : null,
                            IsNullable = IsNullable(b)
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
                                                    .Where(a => string.Equals(a.ModelName, modelName,
                                                         StringComparison.InvariantCultureIgnoreCase))
                                                    .OrderBy(a => a.Type).ToList()
                });
            }

            return AsyncTaskResult.Failed<ModelDisplaySuitVM>($"Model:[{modelName}] 未找到");
        }

        private bool PropertyIsEnum(PropertyInfo property)
        {
            if (property.PropertyType.IsEnum) return true;

            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var type = property.PropertyType.GetGenericArguments().FirstOrDefault();
                return type?.IsEnum ?? false;
            }

            return false;
        }

        private static bool IsNullable(PropertyInfo property)
        {
            if (property.GetCustomAttribute<RequiredAttribute>() != null) return false;

            return property.PropertyType.IsGenericType &&
                   property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                   || property.PropertyType.IsClass;
        }

        private DataType GetDataType(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<DataTypeAttribute>();
            if (attr != null) return attr.DataType;
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var type = property.PropertyType.GetGenericArguments().FirstOrDefault();
            }

            return DataType.Text;
        }
    }
}