using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Rainbow.ViewModels;
using Yunyong.Core;

namespace Rainbow.Services
{
    public class EnumDisplayQueryService : IEnumDisplayQueryService
    {
        private readonly Dictionary<string, EnumDisplayVM> _enumDisplayDic = new Dictionary<string, EnumDisplayVM>();

        public EnumDisplayQueryService()
        {
            var enums = Assembly.Load("Rainbow.Common").GetTypes().Where(a => a.IsEnum);
            foreach (var @enum in enums) Register(@enum);
        }

        public AsyncTaskTResult<List<EnumDisplayVM>> GetEnumDisplayList()
        {
            return AsyncTaskResult.Success(_enumDisplayDic.Values.ToList());
        }

        public AsyncTaskTResult<EnumDisplayVM> GetEnumDisplay(string name)
        {
            if (_enumDisplayDic.TryGetValue(name, out var value)) return AsyncTaskResult.Success(value);
            return AsyncTaskResult.Failed<EnumDisplayVM>("类型不存在或名称有误");
        }

        public void Register<TEType>(EnumDisplayVM vm) where TEType : Enum
        {
            _enumDisplayDic[typeof(TEType).Name] = vm;
        }

        public void Register<TEType>(Dictionary<TEType, string> dataDic, string display = default)
            where TEType : Enum
        {
            var type = typeof(TEType);
            Register<DataType>(new EnumDisplayVM
            {
                Name = type.Name,
                FullName = type.FullName,
                DisplayName = display ?? typeof(TEType).Namespace,
                Fields = dataDic.Select(a => new EnumFieldDisplayVM
                {
                    Name = Enum.GetName(typeof(DataType), a.Key),
                    DisplayName = a.Value,
                    Value = (int) Convert.ChangeType(a.Key, TypeCode.Int32)
                })
            });
        }

        public void Register<TEType>() where TEType : Enum
        {
            var type = typeof(TEType);
            Register(type);
        }

        private void Register(Type type)
        {
            var vm = new EnumDisplayVM
            {
                Name = type.Name,
                FullName = type.FullName,
                DisplayName = type.Name,
                Fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(a => !Equals((int) a.GetValue(null), 0))
                    .Select(
                        a => new EnumFieldDisplayVM
                        {
                            Name = a.Name,
                            DisplayName = a.GetCustomAttribute<DisplayAttribute>()?.Name ?? a.Name,
                            Value = (int) a.GetValue(null)
                        })
            };

            _enumDisplayDic[type.Name] = vm;
        }
    }
}