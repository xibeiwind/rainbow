using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Rainbow.Common;
using TypeLite;
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
            var types = new List<Type>
            {
                typeof(AsyncTaskResult),
                typeof(AsyncTaskTResult<>),
                typeof(PagingList<>)
            };

            //GenerateTypeScriptContracts(typeof(UserVM).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}", types.ToArray());
            //GenerateAngularTypeScriptServices(typeof(AccountController).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}\services");

            GenerateTypeScriptContracts(Assembly.Load("Rainbow.ViewModels"),
                $@"{Path.Combine(args[0], @"Rainbow.Platform.WebAPP\ClientApp\src\app")}", types.ToArray());
            //GenerateAngularTypeScriptServices(Assembly.Load("Rainbow.Platform.Controllers"),
            //    $@"{Path.Combine(args[0], @"Rainbow.Platform.WebAPP\ClientApp\src\app")}\services");

            GenerateAngularTypeScriptServicesV2(Assembly.Load("Rainbow.Platform.Controllers"),
                $@"{Path.Combine(args[0], @"Rainbow.Platform.WebAPP\ClientApp\src\app")}\services");
        }


        private static void GenerateAngularTypeScriptServicesV2(Assembly assembly, string outputPath)
        {
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            var models = assembly.GetTypes().Where(a =>
                a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Controller)) &&
                a.GetCustomAttribute<SkipTSAttribute>() == null);

            var modelServiceNames = new List<string>();

            foreach (var model in models)
            {
                var attr = model.GetCustomAttribute<RouteAttribute>();
                var serviceName = Regex.Match(model.Name, "(.*)Controller").Groups[1].Value + "Service";
                modelServiceNames.Add(serviceName);

                var urlBase = attr != null
                    ? attr.Template.Replace("[controller]", Regex.Match(model.Name, "(.*)Controller").Groups[1].Value)
                    : serviceName;

                var actions = model.GetMethods(BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance |
                                               BindingFlags.DeclaredOnly);

                var template = GetTemplate("Service");
                var presentActions = new List<RainbowAction>();

                foreach (var methodInfo in actions)
                {
                    var routeAttr = methodInfo.GetCustomAttribute<RouteAttribute>();
                    routeAttr = routeAttr ?? new RouteAttribute("");

                    var methodName = methodInfo.Name; //routeAttr.Template.Replace("/", "");

                    if (Regex.IsMatch(methodName, "{"))
                        methodName = Regex.Match(methodName, @"(.*){").Groups[1].Value;

                    var description = methodInfo.GetCustomAttribute<DisplayAttribute>()?.Name?? methodName;

                    var url = $"{urlBase}/{routeAttr.Template}".Replace("{", "${");

                    var returnStr = GetReturnTypeString(methodInfo);

                    var argParamsStr = GetArgumentParamString(methodInfo);
                    var argsStr = GetArgumentsString(methodInfo);
                    var isBaseType = !IsClassArgumnets(methodInfo);

                    void AppendAction(string method)
                    {
                        presentActions.Add(new RainbowAction
                        {
                            Name = methodName,
                            Description = description,
                            Method = method,
                            ReturnStr = returnStr,
                            ArgParamsStr = argParamsStr,
                            ArgsStr = argsStr,
                            Url = url.TrimEnd('/'),
                            IsBaseType = isBaseType
                        });
                    }


                    {
                        var a = methodInfo.GetCustomAttribute<HttpGetAttribute>();
                        if (a != null) AppendAction("get");
                    }
                    {
                        var a = methodInfo.GetCustomAttribute<HttpPostAttribute>();
                        if (a != null) AppendAction("post");
                    }
                    {
                        var a = methodInfo.GetCustomAttribute<HttpPutAttribute>();
                        if (a != null) AppendAction("put");
                    }
                    {
                        var a = methodInfo.GetCustomAttribute<HttpDeleteAttribute>();
                        if (a != null) AppendAction("delete");
                    }
                }

                template = template.Replace("$ModelServiceName$", serviceName)
                    .Replace("$ServiceMethods$", string.Join("\r\n", presentActions.Select(GetServiceActionMethodString)));

                Console.WriteLine(template);

                File.WriteAllText(Path.Combine(outputPath, $"{serviceName}.ts"), template);
            }

            {
                var importServices = modelServiceNames.Select(a => $"import {{ {a} }} from './{a}';");
                var providerServices = modelServiceNames.Select(a => $"    {a},");
                var template = GetTemplate("ServiceModule")
                    .Replace("$ImportServices$", string.Join("\r\n", importServices))
                    .Replace("$ProviderServices$", string.Join("\r\n", providerServices));

                File.WriteAllText(Path.Combine(outputPath, $"service.module.ts"), template);

            }
        }

        private static string GetServiceActionMethodString(RainbowAction action)
        {
            var template = "";

            if (action.Name == "UploadPictureFile")
            {
                template = @"
  public UpLoadPictureFile(data: FormData)
    : Observable<Rainbow.ViewModels.Utils.PictureFileVM> {
    return this.http.post<Rainbow.ViewModels.Utils.PictureFileVM>(`${this.baseUrl}api/ImageUpload/UpLoadPictureFile`, data, getHttpOptions());
  }";
            }

            else if (action.Name == "Logout")
            {
                template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}`, {}, getHttpOptions());
  }
";
                    //    .Replace("{action.Name}", action.Name)
                    //    .Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                    //    .Replace("{action.ReturnStr}", action.ReturnStr)
                    //    .Replace("{action.Method}", action.Method)
                    //    .Replace("{action.ReturnStr}", action.ReturnStr)
                    //    .Replace("{action.Url}", action.Url)
                    //;
            }

            else if (action.IsBaseType && (action.Method == "get" || action.Method == "delete"))
            {
                if (string.IsNullOrWhiteSpace(action.ArgParamsStr))
                {
                    template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}`, getHttpOptions());
  }";
                        //.Replace("{action.Name}", action.Name)
                        //.Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                        //.Replace("{action.ReturnStr}", action.ReturnStr)
                        //.Replace("{action.Method}", action.Method)
                        //.Replace("{action.ReturnStr}", action.ReturnStr)
                        //.Replace("{action.Url}", action.Url);
                }
                else if (action.Url.Contains("${"))
                {
                    template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}`, getHttpOptions());
  }";

                    //.Replace("{action.Name}", action.Name)
                    //.Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                    //.Replace("{action.ReturnStr}", action.ReturnStr)
                    //.Replace("{action.Method}", action.Method)
                    //.Replace("{action.ReturnStr}", action.ReturnStr)
                    //.Replace("{action.Url}", action.Url);

                }
                else
                {
                    template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}?${stringify({action.ArgsStr})}`, getHttpOptions());
  }";
                        //.Replace("{action.Name}", action.Name)
                        //.Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                        //.Replace("{action.ReturnStr}", action.ReturnStr)
                        //.Replace("{action.Method}", action.Method)
                        //.Replace("{action.ReturnStr}", action.ReturnStr)
                        //.Replace("{action.ArgsStr}", action.ArgsStr)
                        //.Replace("{action.Url}", action.Url);


                }
            }

            else if (action.Method == "get")
            {
                template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}?${stringify({action.ArgsStr})}`, getHttpOptions());
  }";
                    //.Replace("{action.Name}", action.Name)
                    //.Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                    //.Replace("{action.ReturnStr}", action.ReturnStr)
                    //.Replace("{action.Method}", action.Method)
                    //.Replace("{action.ReturnStr}", action.ReturnStr)
                    //.Replace("{action.ArgsStr}", action.ArgsStr)
                    //.Replace("{action.Url}", action.Url);
            }

            else if (action.Method == "delete")
            {
                template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}?${stringify({action.ArgsStr})}`, getHttpOptions());
  }";
                //.Replace("{action.Name}", action.Name)
                //.Replace("{action.ArgParamsStr}", action.ArgParamsStr)
                //.Replace("{action.ReturnStr}", action.ReturnStr)
                //.Replace("{action.Method}", action.Method)
                //.Replace("{action.ReturnStr}", action.ReturnStr)
                //.Replace("{action.ArgsStr}", action.ArgsStr)
                //.Replace("{action.Url}", action.Url);
            }
            else
            {
                template = @"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${this.baseUrl}{action.Url}`, {action.ArgsStr}, getHttpOptions());
  }";}

            template = template
            .Replace("{action.Name}", action.Name)
            .Replace("{action.ArgParamsStr}", action.ArgParamsStr)
            .Replace("{action.ReturnStr}", action.ReturnStr)
            .Replace("{action.Method}", action.Method)
            .Replace("{action.ReturnStr}", action.ReturnStr)
            .Replace("{action.ArgsStr}", string.IsNullOrEmpty(action.ArgsStr) ? "{}" : action.ArgsStr)
            .Replace("{action.Url}", action.Url);

            var comments = $@"
  /**
   * {action.Description}
   */";
            
            return string.Join("", comments, template);


        }


        private static void GenerateTypeScriptContracts(Assembly assembly, string outputPath, params Type[] extTypes)
        {
            //var assembly = type.Assembly;

            var models = assembly.GetTypes();

            var generator = new TypeScriptFluent()
                .WithConvertor<Guid>(c => "string")
                .WithConvertor<String>(c=>"string")
                .WithConvertor<Boolean>(c=>"boolean");
            
            generator.ModelBuilder.Add<DataType>();

            foreach (var model in models) generator.ModelBuilder.Add(model);

            foreach (var extType in extTypes) generator.ModelBuilder.Add(extType);

            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            //Generate enums
            var tsEnumDefinitions = generator.Generate(TsGeneratorOutput.Enums);
            
            File.WriteAllText(Path.Combine(outputPath, "enums.ts"), tsEnumDefinitions);
            //Generate interface definitions for all classes

            var tsClassStringBuilder = new StringBuilder();
            tsClassStringBuilder.AppendLine("// tslint:disable:no-empty-interface");
            var tsClassDefinitions = generator.Generate(TsGeneratorOutput.Properties | TsGeneratorOutput.Fields);

            tsClassStringBuilder.AppendLine(tsClassDefinitions);
            File.WriteAllText(Path.Combine(outputPath, "classes.d.ts"), tsClassStringBuilder.ToString());
        }

        private static string GetTemplate(string fileName)
        {
            var resName = $"Rainbow.TypeLiteConsoleApp.Template.{fileName}.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }

        private static string GetReturnTypeString(MethodInfo method)
        {
            var respTypeAttr = method.GetCustomAttribute<ProducesDefaultResponseTypeAttribute>();
            if (respTypeAttr != null) return GetTypeString(respTypeAttr.Type);


            return "any";
        }

        private static string GetTypeString(Type type)
        {
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return $@"{
                        string.Join(", ",
                            type.GenericTypeArguments.Select(a => a.FullName))}[]";


                if (type.GetGenericTypeDefinition() == typeof(List<>))
                    return $@"{
                        string.Join(", ",
                            type.GenericTypeArguments.Select(GetTypeString))}[]";


                var t = type.GetGenericTypeDefinition();
                var name = t.FullName.Substring(0, t.FullName.IndexOf("`", StringComparison.Ordinal));
                return $@"{name}<{
                    string.Join(", ",
                        type.GenericTypeArguments.Select(GetTypeString))}>";

            }

            if (type == typeof(Guid)) return "string";

            {
                var types = new[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                };
                if (types.Contains(type)) return "number";
            }
            if (type == typeof(string)) return "string";

            if (type == typeof(bool)) return "boolean";

            return type.FullName;
        }

        private static string GetArgumentParamString(MethodInfo method)
        {
            var args = method.GetParameters();

            return string.Join(", ", args.Select(a => $"{a.Name}: {GetTypeString(a.ParameterType)}"));
        }

        private static string GetArgumentsString(MethodInfo method)
        {
            var args = method.GetParameters();
            return string.Join(", ", args.Select(a => a.Name));
        }

        private static bool IsClassArgumnets(MethodInfo method)
        {
            //return false;
            var baseTypes = new[]
            {
                typeof(int),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(Guid),
                typeof(string),
                typeof(bool),
                typeof(List<>),
                typeof(IEnumerable<>),
                typeof(Array)
            };

            var args = method.GetParameters();
            if (args.Length != 1) return false;

            if (baseTypes.Contains(args[0].ParameterType) ||
                args[0].ParameterType.IsGenericType &&
                baseTypes.Contains(args[0].ParameterType.GetGenericTypeDefinition())
            ) // baseTypes.Contains(args[0].ParameterType.GetGenericTypeDefinition()))
                return false;

            return true;
        }
    }
}