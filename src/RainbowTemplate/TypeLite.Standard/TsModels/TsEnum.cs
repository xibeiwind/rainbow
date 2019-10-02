
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TypeLite.Extensions;

namespace TypeLite.TsModels
{
    /// <summary>
    ///     Represents an enum in the code model.
    /// </summary>
    public class TsEnum : TsModuleMember
    {
        /// <summary>
        ///     Initializes a new instance of the TsEnum class with the specific CLR enum.
        /// </summary>
        /// <param name="type">The CLR enum represented by this instance of the TsEnum.</param>
        public TsEnum(Type type)
            : base(type)
        {
            if (!Type.IsEnum) throw new ArgumentException("ClrType isn't enum.");

            Values = new List<TsEnumValue>(GetEnumValues(type));

            var attribute = Type.GetCustomAttribute<TsEnumAttribute>(false);
            if (attribute != null)
            {
                if (!string.IsNullOrEmpty(attribute.Name)) Name = attribute.Name;

                if (!string.IsNullOrEmpty(attribute.Module)) Module.Name = attribute.Module;
            }
        }

        /// <summary>
        ///     Gets or sets bool value indicating whether this enum will be ignored by TsGenerator.
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        ///     Gets collection of properties of the class.
        /// </summary>
        public ICollection<TsEnumValue> Values { get; }

        /// <summary>
        ///     Retrieves a collection of possible value of the enum.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <returns>collection of all enum values.</returns>
        protected IEnumerable<TsEnumValue> GetEnumValues(Type enumType)
        {
            var attribute = enumType.GetCustomAttribute<JsonConverterAttribute>();
            if (attribute?.ConverterType == typeof(StringEnumConverter))
            {
                return enumType.GetFields()
                               .Where(fieldInfo => fieldInfo.IsLiteral && !string.IsNullOrEmpty(fieldInfo.Name))
                               .Select(fieldInfo => new TsEnumValue(fieldInfo, $"'{fieldInfo.Name}'"));
            }
            else
            {
                return enumType.GetFields()
                    .Where(fieldInfo => fieldInfo.IsLiteral && !string.IsNullOrEmpty(fieldInfo.Name))
                    .Select(fieldInfo => new TsEnumValue(fieldInfo));
            }


        }
    }
}