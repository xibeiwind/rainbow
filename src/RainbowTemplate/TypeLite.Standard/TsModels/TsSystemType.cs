﻿using System;
using TypeLite.Extensions;

namespace TypeLite.TsModels
{
    /// <summary>
    ///     Represents a system type in the code model.
    /// </summary>
    public class TsSystemType : TsType
    {
        /// <summary>
        ///     Initializes a new instance of the TsSystemType with the specific CLR type.
        /// </summary>
        /// <param name="type">The CLR type represented by this instance of the TsSystemType.</param>
        public TsSystemType(Type type)
            : base(type)
        {
            if (type.IsNullable()) type = type.GetNullableValueType();

            switch (type.Name)
            {
                case "Boolean":
                    Kind = SystemTypeKind.Bool;
                    break;
                case "String":
                case "Char":
                    Kind = SystemTypeKind.String;
                    break;
                case "Byte":
                case "SByte":
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Single":
                case "Double":
                case "Decimal":
                case "IntPtr":
                case "UIntPtr":
                    Kind = SystemTypeKind.Number;
                    break;
                case "DateTime":
                case "DateTimeOffset":
                    Kind = SystemTypeKind.Date;
                    break;
                default:
                    throw new ArgumentException(string.Format("The type '{0}' is not supported system type.",
                        type.FullName));
            }
        }

        /// <summary>
        ///     Gets kind of the system type.
        /// </summary>
        public SystemTypeKind Kind { get; }
    }
}