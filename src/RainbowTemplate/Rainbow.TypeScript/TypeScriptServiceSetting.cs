using System.Reflection;

namespace Rainbow.TypeScript
{
    public class TypeScriptServiceSetting
    {
        public Assembly Assembly { get; set; }
        public TypeScriptServiceType ServiceType { get; set; }
        public string OutputPath { get; set; }
    }
}