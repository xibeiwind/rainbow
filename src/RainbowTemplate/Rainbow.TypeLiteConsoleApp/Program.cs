using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Rainbow.TypeScript;
using Yunyong.Core;

//#if (EnableIdentity)
//using Rainbow.Platform.Controllers;
//#endif

namespace Rainbow.TypeLiteConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var helper = new TypeScriptServiceHelper();

            var types = new List<Type>
            {
                typeof(AsyncTaskResult),
                typeof(AsyncTaskTResult<>),
                typeof(PagingList<>)
            };

            helper.GenerateTypeScriptContracts( new ContractSetting()
            {
                Assembly = Assembly.Load("Rainbow.ViewModels"),
                OutputPath = $@"{Path.Combine(args[0], @"Rainbow.Platform.WebAPP\ClientApp\src\app")}",
                ExtTypes = types
            });

            helper.GenerateTypeScriptServices(new TypeScriptServiceSetting(Assembly.Load("Rainbow.Platform.Controllers"), TypeScriptServiceType.Angular, $@"{Path.Combine(args[0], @"Rainbow.Platform.WebAPP\ClientApp\src\app", "service")}"));

            //helper.GenerateTypeScriptContracts(Assembly.Load("Rainbow.ViewModels"),
            //    $@"{Path.Combine(args[0], @"VueServiceTS")}", types.ToArray());

            //helper.GenerateTypeScriptServices(Assembly.Load("Rainbow.Platform.Controllers"), TypeScriptServiceType.Vue,
            //    $@"{Path.Combine(args[0], @"VueServiceTS", "service")}");
        }
    }
}