using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TypeLite.Extensions;

namespace TypeLite.TsModels
{
    /// <summary>
    ///     Represents a property of the class in the code model.
    /// </summary>
    [DebuggerDisplay("Name: {Name}")]
    public class TsProperty
    {
        /// <summary>
        ///     Initializes a new instance of the TsProperty class with the specific CLR property.
        /// </summary>
        /// <param name="memberInfo">The CLR property represented by this instance of the TsProperty.</param>
        public TsProperty(PropertyInfo memberInfo)
        {
            MemberInfo = memberInfo;
            Name = memberInfo.Name;
            Description = memberInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? Name;

            var propertyType = memberInfo.PropertyType;
            if (propertyType.IsNullable())
            {
                //propertyType = propertyType.GetNullableValueType();

                IsOptional = true;
                propertyType = propertyType.GetNullableValueType();
            }
            if (propertyType.IsClass && memberInfo.GetCustomAttribute<RequiredAttribute>() == null)
            {
                IsOptional = true;
            }
            GenericArguments = propertyType.IsGenericType
                ? propertyType.GetGenericArguments().Select(o => new TsType(o)).ToArray()
                : new TsType[0];

            PropertyType = propertyType.IsEnum ? new TsEnum(propertyType) : new TsType(propertyType);

            var attribute = memberInfo.GetCustomAttribute<TsPropertyAttribute>(false);
            if (attribute != null)
            {
                if (!string.IsNullOrEmpty(attribute.Name)) Name = attribute.Name;

                IsOptional = attribute.IsOptional;
            }

            IsIgnored = memberInfo.GetCustomAttribute<TsIgnoreAttribute>(false) != null;

            // Only fields can be constants.
            ConstantValue = null;
        }

        /// <summary>
        ///     Initializes a new instance of the TsProperty class with the specific CLR field.
        /// </summary>
        /// <param name="memberInfo">The CLR field represented by this instance of the TsProperty.</param>
        public TsProperty(FieldInfo memberInfo)
        {
            MemberInfo = memberInfo;
            Name = memberInfo.Name;
            Description = memberInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? Name;
            if (memberInfo.ReflectedType.IsGenericType)
            {
                var definitionType = memberInfo.ReflectedType.GetGenericTypeDefinition();
                var definitionTypeProperty = definitionType.GetProperty(memberInfo.Name);
                if (definitionTypeProperty.PropertyType.IsGenericParameter)
                    PropertyType = TsType.Any;
                else
                    PropertyType = memberInfo.FieldType.IsEnum
                        ? new TsEnum(memberInfo.FieldType)
                        : new TsType(memberInfo.FieldType);
            }
            else
            {
                var propertyType = memberInfo.FieldType;
                if (propertyType.IsNullable()) propertyType = propertyType.GetNullableValueType();

                PropertyType = propertyType.IsEnum ? new TsEnum(propertyType) : new TsType(propertyType);
            }

            var attribute = memberInfo.GetCustomAttribute<TsPropertyAttribute>(false);
            if (attribute != null)
            {
                if (!string.IsNullOrEmpty(attribute.Name)) Name = attribute.Name;

                IsOptional = attribute.IsOptional;
            }

            IsIgnored = memberInfo.GetCustomAttribute<TsIgnoreAttribute>(false) != null;

            if (memberInfo.IsLiteral && !memberInfo.IsInitOnly)
                ConstantValue = memberInfo.GetValue(null);
            else
                ConstantValue = null;
        }

        public string Name { get; }
        public string Description { get; }

        /// <summary>
        ///     Gets or sets type of the property.
        /// </summary>
        public TsType PropertyType { get; set; }

        /// <summary>
        ///     Gets the CLR property represented by this TsProperty.
        /// </summary>
        public MemberInfo MemberInfo { get; set; }

        /// <summary>
        ///     Gets or sets bool value indicating whether this property will be ignored by TsGenerator.
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        ///     Gets or sets bool value indicating whether this property is optional in TypeScript interface.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        ///     Gets the GenericArguments for this property.
        /// </summary>
        public IList<TsType> GenericArguments { get; }

        /// <summary>
        ///     Gets or sets the constant value of this property.
        /// </summary>
        public object ConstantValue { get; set; }
    }
}