using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TypeLite.TsModels
{
    /// <summary>
    ///     Represents a value of the enum
    /// </summary>
    public class TsEnumValue
    {
        /// <summary>
        ///     Initializes a new instance of the TsEnumValue class.
        /// </summary>
        public TsEnumValue()
        {
        }

        public TsEnumValue(FieldInfo field, string value)
        {
            Field = field;
            Name = field.Name;
            Description = field.GetCustomAttribute<DisplayAttribute>()?.Name ?? field.Name;


            Value = value;
        }

        /// <summary>
        ///     Initializes a new instance of the TsEnumValue class with the specific name and value.
        /// </summary>
        public TsEnumValue(FieldInfo field)
        {
            Field = field;
            Name = field.Name;
            Description = field.GetCustomAttribute<DisplayAttribute>()?.Name ?? field.Name;

            var value = field.GetValue(null);

            var valueType = Enum.GetUnderlyingType(value.GetType());
            if (valueType == typeof(byte)) Value = ((byte) value).ToString();
            if (valueType == typeof(sbyte)) Value = ((sbyte) value).ToString();
            if (valueType == typeof(short)) Value = ((short) value).ToString();
            if (valueType == typeof(ushort)) Value = ((ushort) value).ToString();
            if (valueType == typeof(int)) Value = ((int) value).ToString();
            if (valueType == typeof(uint)) Value = ((uint) value).ToString();
            if (valueType == typeof(long)) Value = ((long) value).ToString();
            if (valueType == typeof(ulong)) Value = ((ulong) value).ToString();
        }

        /// <summary>
        ///     Gets or sets name of the enum value
        /// </summary>
        public string Name { get; }

        public string Description { get; }

        /// <summary>
        ///     Gets or sets value of the enum
        /// </summary>
        public string Value { get; }

        public FieldInfo Field { get; }
    }
}