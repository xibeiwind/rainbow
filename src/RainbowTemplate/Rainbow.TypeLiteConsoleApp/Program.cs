using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rainbow.Common;
#if (EnableIdentity)
using Rainbow.Platform.Controllers;
using Rainbow.ViewModels.Users;
#endif

using TypeLite;
using Yunyong.Core;

namespace Rainbow.TypeLiteConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var types = new List<Type>
            {
                typeof(AsyncTaskResult),
                typeof(AsyncTaskTResult<>),
                typeof(PagingList<>)
            }; //typeof(OrderState).Assembly.GetTypes().Where(a => a.IsEnum).ToList();

            //GenerateTypeScriptContracts(typeof(UserVM).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}", types.ToArray());
            //GenerateAngularTypeScriptServices(typeof(AccountController).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}\services");

            GenerateTypeScriptContracts(typeof(UserVM).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}", types.ToArray());
            GenerateAngularTypeScriptServices(typeof(AccountController).Assembly, $@"{Path.Combine(args[0], $@"Rainbow.Platform.WebAPP\ClientApp\src\app")}\services");


        }

        private static void GenerateTypeScriptContracts(Assembly assembly, string outputPath, params Type[] extTypes)
        {
            //var assembly = type.Assembly;

            var models = assembly.GetTypes();

            var generator = new TypeScriptFluent()
                .WithConvertor<Guid>(c => "string");

            generator.ModelBuilder.Add<DataType>();

            foreach (var model in models)
            {
                generator.ModelBuilder.Add(model);
            }

            foreach (var extType in extTypes)
            {
                generator.ModelBuilder.Add(extType);
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }


            //Generate enums
            var tsEnumDefinitions = generator.Generate(TsGeneratorOutput.Enums);
            File.WriteAllText(Path.Combine(outputPath, "enums.ts"), tsEnumDefinitions);
            //Generate interface definitions for all classes
            var tsClassDefinitions = generator.Generate(TsGeneratorOutput.Properties | TsGeneratorOutput.Fields);
            File.WriteAllText(Path.Combine(outputPath, "classes.d.ts"), tsClassDefinitions);
        }

        private static void GenerateAngularTypeScriptContracts(Type type, string outputPath, params Type[] extTypes)
        {
            var assembly = type.Assembly;
            // If you want a subset of classes from this assembly, filter them here
            var models = assembly.GetTypes();

            var generator = new TypeScriptFluent()
                //.WithFormatter(new TsSimpleMemberTypeFormatter())
                .WithConvertor<Guid>(c => "string");

            foreach (var model in models)
            {
                generator.ModelBuilder.Add(model);
            }

            foreach (var extType in extTypes)
            {
                generator.ModelBuilder.Add(extType);
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            //Generate enums
            var tsEnumDefinitions = generator.Generate(TsGeneratorOutput.Enums);
            File.WriteAllText(Path.Combine(outputPath, "enums.ts"), tsEnumDefinitions);
            //Generate interface definitions for all classes
            var tsClassDefinitions = generator.Generate(TsGeneratorOutput.Properties | TsGeneratorOutput.Fields);
            File.WriteAllText(Path.Combine(outputPath, "classes.d.ts"), tsClassDefinitions);
        }

        private static void GenerateTypeScriptServices(Assembly assembly, string outputPath)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            //var assembly = Assembly.LoadFrom(assemblyFile);
            // If you want a subset of classes from this assembly, filter them here
            var models = assembly.GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Controller))).ToList();

            foreach (var model in models)
            {
                Console.WriteLine(model.Name);
                var attr = model.GetCustomAttribute<RouteAttribute>();

                var serviceName = Regex.Match(model.Name, "(.*)Controller").Groups[1].Value + "Service";

                Console.WriteLine(serviceName);
                var urlBase = attr != null ? attr.Template.Replace("[controller]", Regex.Match(model.Name, "(.*)Controller").Groups[1].Value) : serviceName;

                var actions = model.GetMethods(BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                Console.WriteLine(actions.Length);

                var list = new List<RainbowAction>();

                foreach (var methodInfo in actions)
                {
                    Console.WriteLine(methodInfo.Name);

                    var routeAttr = methodInfo.GetCustomAttribute<RouteAttribute>();
                    if (routeAttr != null)
                    {
                        var methodName = routeAttr.Template.Replace("/", "");

                        if (Regex.IsMatch(methodName, "{"))
                        {
                            methodName = Regex.Match(methodName, @"(.*){").Groups[1].Value;
                        }

                        //Console.WriteLine(methodName);

                        var url = $"{urlBase}/{routeAttr.Template}".Replace("{", "${");
                        //Console.WriteLine(url);

                        var returnStr = GetReturnTypeString(methodInfo);

                        var argParamsStr = GetArgumentParamString(methodInfo);
                        var argsStr = GetArgumentsString(methodInfo);
                        var isBaseType = !IsClassArgumnets(methodInfo);

                        void AppendAction(string method)
                        {
                            list.Add(new RainbowAction
                            {
                                Name = methodName,
                                Method = method,
                                ReturnStr = returnStr,
                                ArgParamsStr = argParamsStr,
                                ArgsStr = argsStr,
                                Url = url,
                                IsBaseType = isBaseType
                            });
                        }

                        {
                            var a = methodInfo.GetCustomAttribute<HttpGetAttribute>();
                            if (a != null)
                            {
                                AppendAction("get");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpPostAttribute>();
                            if (a != null)
                            {
                                AppendAction("post");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpPutAttribute>();
                            if (a != null)
                            {
                                AppendAction("put");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpDeleteAttribute>();
                            if (a != null)
                            {
                                AppendAction("delete");
                            }
                        }
                    }
                }

                Console.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));

                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($@"
/* eslint-disable */
/* tslint:disable */

    export class {serviceName} {{
        constructor(private directives: any){{}}
");
                foreach (var action in list)
                {
                    //            if (action.IsBaseType && (action.Method == "get" || action.Method == "delete"))
                    //            {
                    //                strBuilder.AppendLine($@"
                    //public async {action.Method}{action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
                    //    return (await this.directives.{action.Method}Async(`{action.Url}`)) as {action.ReturnStr};
                    //}}");
                    //            }
                    //            else
                    //            {
                    //                strBuilder.AppendLine($@"
                    //public async {action.Method}{action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
                    //    return (await this.directives.{action.Method}Async(`{action.Url}?${{stringify({action.ArgsStr})}}`)) as {action.ReturnStr};
                    //}}");
                    //            }

                    if (action.IsBaseType && (action.Method == "get" || action.Method == "delete"))
                    {
                        if (string.IsNullOrWhiteSpace(action.ArgParamsStr))
                        {
                            strBuilder.AppendLine($@"
    public async {action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
        return await this.directives.{action.Method}Async(`{action.Url}`) as {action.ReturnStr};
    }}");
                        }
                        else
                        {
                            strBuilder.AppendLine($@"
    public async {action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
        return await this.directives.{action.Method}Async(`{action.Url}`) as {action.ReturnStr};
    }}");
                        }

                    }
                    else if (action.Method == "get")
                    {
                        strBuilder.AppendLine($@"
    public async {action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
        return await this.directives.{action.Method}Async(`{action.Url}`, {action.ArgsStr}) as {action.ReturnStr};
    }}");
                    }
                    else if (action.Method == "delete")
                    {
                        strBuilder.AppendLine($@"
    public async {action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
        return await this.directives.{action.Method}Async(`${action.Url}`, {action.ArgsStr}) as {action.ReturnStr};
    }}");
                    }
                    else
                    {
                        strBuilder.AppendLine($@"
    public async {action.Name}({action.ArgParamsStr}): Promise<{action.ReturnStr}> {{
        return await this.directives.{action.Method}Async(`${action.Url}`, {action.ArgsStr??"{}"}) as {action.ReturnStr};
    }}");
                    }
                }

                strBuilder.AppendLine($@"
    }}
");
                File.WriteAllText(Path.Combine(outputPath, $"{serviceName}.ts"), strBuilder.ToString());
            }
        }

        private static void GenerateAngularTypeScriptServices(Assembly assembly, string outputPath)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            //var assembly = Assembly.LoadFrom(assemblyFile);

            var models = assembly.GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Controller)) && a.GetCustomAttribute<SkipTSAttribute>() == null);

            foreach (var model in models)
            {
                Console.WriteLine(model.Name);
                var attr = model.GetCustomAttribute<RouteAttribute>();

                var serviceName = Regex.Match(model.Name, "(.*)Controller").Groups[1].Value + "Service";

                Console.WriteLine(serviceName);
                var urlBase = attr != null ? attr.Template.Replace("[controller]", Regex.Match(model.Name, "(.*)Controller").Groups[1].Value) : serviceName;

                var actions = model.GetMethods(BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                Console.WriteLine(actions.Length);

                var list = new List<RainbowAction>();

                foreach (var methodInfo in actions)
                {
                    Console.WriteLine(methodInfo.Name);

                    var routeAttr = methodInfo.GetCustomAttribute<RouteAttribute>();
                    routeAttr = routeAttr ?? new RouteAttribute("");
                    if (routeAttr != null)
                    {
                        var methodName = methodInfo.Name; //routeAttr.Template.Replace("/", "");

                        if (Regex.IsMatch(methodName, "{"))
                        {
                            methodName = Regex.Match(methodName, @"(.*){").Groups[1].Value;
                        }

                        //Console.WriteLine(methodName);

                        var url = $"{urlBase}/{routeAttr.Template}".Replace("{", "${");
                        //Console.WriteLine(url);

                        var returnStr = GetReturnTypeString(methodInfo);

                        var argParamsStr = GetArgumentParamString(methodInfo);
                        var argsStr = GetArgumentsString(methodInfo);
                        var isBaseType = !IsClassArgumnets(methodInfo);

                        void AppendAction(string method)
                        {
                            list.Add(new RainbowAction
                            {
                                Name = methodName,
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
                            if (a != null)
                            {
                                AppendAction("get");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpPostAttribute>();
                            if (a != null)
                            {
                                AppendAction("post");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpPutAttribute>();
                            if (a != null)
                            {
                                AppendAction("put");
                            }
                        }
                        {
                            var a = methodInfo.GetCustomAttribute<HttpDeleteAttribute>();
                            if (a != null)
                            {
                                AppendAction("delete");
                            }
                        }
                    }
                }

                Console.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));

                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($@"
import {{ Injectable, Inject }} from '@angular/core';
import {{ HttpClient }} from '@angular/common/http';
import {{ Observable }} from 'rxjs';
import {{ stringify }} from 'querystring';

@Injectable()
export class {serviceName} {{
  constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string){{ }}");
                foreach (var action in list)
                {
                    if (action.Name == "UpLoadPictureFile")
                    {
                        strBuilder.AppendLine(@"
  public UpLoadPictureFile(data: FormData)
    : Observable<Rainbow.ViewModels.Utils.PictureFileVM> {
    return this.http.post<Rainbow.ViewModels.Utils.PictureFileVM>(`${this.baseUrl}api/ImageUpload/UpLoadPictureFile`, data);
  }");
                    }
                    else if (action.Name == "Logout")
                    {
                        strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}`,{{}});
  }}");
                    }
                    else if (action.IsBaseType && (action.Method == "get" || action.Method == "delete"))
                    {
                        if (string.IsNullOrWhiteSpace(action.ArgParamsStr))
                        {
                            strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}`);
  }}");
                        }
                        else
                        {
                            if (action.Url.Contains("${"))
                            {
                                strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}`);
  }}");
                            }
                            else
                            {
                                strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}?${{stringify({action.ArgsStr})}}`);
  }}");
                            }

                        }

                    }
                    else if (action.Method == "get")
                    {
                        strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}?${{stringify({action.ArgsStr})}}`);
  }}");
                    }
                    else if (action.Method == "delete")
                    {
                        strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}?${{stringify({action.ArgsStr})}}`);
  }}");
                    }
                    else
                    {
                        strBuilder.AppendLine($@"
  public {action.Name}({action.ArgParamsStr})
    : Observable<{action.ReturnStr}> {{
    return this.http.{action.Method}<{action.ReturnStr}>
      (`${{this.baseUrl}}{action.Url}`, {(string.IsNullOrEmpty( action.ArgsStr)?"{}" : action.ArgsStr)});
  }}");
                    }
                }

                strBuilder.AppendLine($@"
}}
");
                File.WriteAllText(Path.Combine(outputPath, $"{serviceName}.ts"), strBuilder.ToString());
            }


            void GenerateServiceModule(IEnumerable<Type> serviceTypes)
            {


                var serviceModuleTemplate = $@"
import {{ NgModule }} from '@angular/core';
import {{ CommonModule }} from '@angular/common';
import {{ SiteService }} from './site.service';

$ImportServices$


@NgModule({{
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    SiteService,
$ProviderServices$
  ],
}})
export class ServiceModule {{ }}
";
                var importServices = string.Join("\r\n", serviceTypes.Select(a =>
                {
                    var serviceName = Regex.Match(a.Name, "(.*)Controller").Groups[1].Value + "Service";
                    return $"import {{ {serviceName} }} from './{serviceName}';";
                }));

                var providerServices = string.Join("\r\n", serviceTypes.Select(a =>
                {
                    var serviceName = Regex.Match(a.Name, "(.*)Controller").Groups[1].Value + "Service";

                    return $"    {serviceName},";
                }));

                var code = serviceModuleTemplate.Replace("$ImportServices$", importServices)
                    .Replace("$ProviderServices$", providerServices);

                File.WriteAllText(Path.Combine(outputPath, "service.module.ts"), code, Encoding.UTF8);
            }

            GenerateServiceModule(models);


        }

        private static string GetReturnTypeString(MethodInfo method)
        {
            var respTypeAttr = method.GetCustomAttribute<ProducesDefaultResponseTypeAttribute>();
            if (respTypeAttr != null)
            {
                return GetTypeString(respTypeAttr.Type);
            }


            return "any";
        }

        private static string GetTypeString(Type type)
        {
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return $@"{
                        string.Join(", ",
                            type.GenericTypeArguments.Select(a => a.FullName))}[]";
                }



                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    return $@"{
                        string.Join(", ",
                            type.GenericTypeArguments.Select(a => GetTypeString(a)))}[]";
                }


                var t = type.GetGenericTypeDefinition();
                var name = t.FullName.Substring(0, t.FullName.IndexOf("`", StringComparison.Ordinal));
                return $@"{name}<{
                    string.Join(", ",
                        type.GenericTypeArguments.Select(a => GetTypeString(a)))}>";

                //if (type.GetGenericTypeDefinition() == typeof(PagingList<>))
                //{
                //    return $@"Yunyong.Core.PagingList<{
                //        string.Join(", ",
                //            type.GenericTypeArguments.Select(a => GetTypeString(a)))}>";

                //}
                //return type.GetGenericTypeDefinition().FullName;
            }

            if (type == typeof(Guid))
            {
                return "String";
            }

            {
                var types = new[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                };
                if (types.Contains(type))
                {
                    return "Number";
                }
            }
            if (type == typeof(string))
            {
                return "String";
            }

            if (type == typeof(bool))
            {
                return "Boolean";
            }

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
            if (args.Length != 1)
            {
                return false;
            }

            if (baseTypes.Contains(args[0].ParameterType) ||
                args[0].ParameterType.IsGenericType && baseTypes.Contains(args[0].ParameterType.GetGenericTypeDefinition())
            ) // baseTypes.Contains(args[0].ParameterType.GetGenericTypeDefinition()))
            {
                return false;
            }

            return true;
        }

    }
}
