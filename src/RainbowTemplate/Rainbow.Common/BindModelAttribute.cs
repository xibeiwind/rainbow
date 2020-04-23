using System;

using Rainbow.Common.Enums;

namespace Rainbow.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BindModelAttribute : Attribute
    {
        public BindModelAttribute(string modelName, VMType type)
        {
            ModelName = modelName;
            Type = type;
        }

        public string ModelName { get; }
        public VMType Type { get; }
    }
}