using System;
using System.Collections;
using System.Diagnostics;

namespace TypeLite.TsModels
{
    /// <summary>
    ///     Represents a collection in the code model.
    /// </summary>
    [DebuggerDisplay("TsCollection - ItemsType={ItemsType}")]
    public class TsCollection : TsType
    {
        /// <summary>
        ///     Initializes a new instance of the TsCollection class with the specific CLR type.
        /// </summary>
        /// <param name="type">The CLR collection represented by this instance of the TsCollection.</param>
        public TsCollection(Type type)
            : base(type)
        {
            var enumerableType = GetEnumerableType(Type);
            if (enumerableType == Type)
                ItemsType = Any;
            else if (enumerableType != null)
                ItemsType = Create(enumerableType);
            else if (typeof(IEnumerable).IsAssignableFrom(Type))
                ItemsType = Any;
            else
                throw new ArgumentException(string.Format("The type '{0}' is not collection.", Type.FullName));

            Dimension = GetCollectionDimension(type);
        }

        /// <summary>
        ///     Gets or sets type of the items in the collection.
        /// </summary>
        /// <remarks>
        ///     If the collection isn't strongly typed, the ItemsType property is initialized to TsType.Any.
        /// </remarks>
        public TsType ItemsType { get; set; }

        /// <summary>
        ///     Gets or sets the dimension of the collection.
        /// </summary>
        public int Dimension { get; set; }

        private static int GetCollectionDimension(Type t)
        {
            Type enumerableUnderlying = null;

            if (t.IsArray) return GetCollectionDimension(t.GetElementType()) + 1;

            if (t != typeof(string) && (enumerableUnderlying = GetEnumerableType(t)) != null)
            {
                if (enumerableUnderlying == t) return 0;
                return GetCollectionDimension(enumerableUnderlying) + 1;
            }

            return 0;
        }
    }
}