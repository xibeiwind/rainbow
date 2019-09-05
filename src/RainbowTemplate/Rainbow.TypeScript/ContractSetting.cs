using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rainbow.TypeScript
{
    public class ContractSetting
    {
        public Assembly Assembly { get; set; }
        public string OutputPath { get; set; }

        public IEnumerable<Type> ExtTypes { get; set; }
    }
}