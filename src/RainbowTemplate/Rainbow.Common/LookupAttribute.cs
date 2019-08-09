using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LookupAttribute :Attribute
    {
        public Type Type { get; }
        public string ValueField { get; }
        public string DisplayField { get; }

        public LookupAttribute(Type type, string valueField, string displayField)
        {
            Type = type;
            ValueField = valueField;
            DisplayField = displayField;
        }
    }
}
