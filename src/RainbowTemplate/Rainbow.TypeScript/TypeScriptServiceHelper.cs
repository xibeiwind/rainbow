using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Common;
using TypeLite;

namespace Rainbow.TypeScript
{
    //public class TypeScriptServiceSetting
    //{
    //    public Assembly Assembly { get; set; }
    //    public string OutputPath { get; set; }

    //    public TypeScriptServiceType ServiceType { get; set; }
    //}

    public class TypeScriptServiceHelper
    {
        public void GenerateTypeScriptServices(TypeScriptServiceSetting setting)
        {
            if (!Directory.Exists(setting.OutputPath)) Directory.CreateDirectory(setting.OutputPath);

            var models = setting.Assembly.GetTypes().Where(a =>
                a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Controller)) &&
                a.GetCustomAttribute<SkipTSAttribute>() == null);

            //var modelServiceNames = new List<string>();

            foreach (var model in models)
            {
                var attr = model.GetCustomAttribute<RouteAttribute>();
                var serviceName = Regex.Match(model.Name, "(.*)Controller").Groups[1].Value + "Service";
                //modelServiceNames.Add(serviceName);

                var urlBase = attr != null
                    ? attr.Template.Replace("[controller]", Regex.Match(model.Name, "(.*)Controller").Groups[1].Value)
                    : serviceName;

                var actions = model.GetMethods(BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance |
                                               BindingFlags.DeclaredOnly);

                var presentActions = actions.Select(GetRainbowAction).ToList();

                var template = GetTemplate($"{setting.ServiceType}.Service");

                var code = template.Replace("$ModelServiceName$", serviceName)
                    .Replace("$ServiceMethods$",
                        string.Join("\r\n",
                            presentActions.Select(a => GetServiceActionMethodString(a, setting.ServiceType))));

                File.WriteAllText(Path.Combine(setting.OutputPath, $"{serviceName}.ts"), code);
            }
        }

        private string GetServiceActionMethodString(RainbowAction action, TypeScriptServiceType type)
        {
            var comments = $@"
  /**
   * {action.Description}
   */
";
            var template = comments + GetActionTemplate(action, type);

            return template.Replace("$action.Name$", action.Name)
                .Replace("$action.ArgParamsStr$", action.ArgParamsStr)
                .Replace("$action.ReturnStr$", action.ReturnStr)
                .Replace("$action.Method$", action.Method)
                .Replace("$action.ReturnStr$", action.ReturnStr)
                .Replace("$action.ArgsStr$", string.IsNullOrEmpty(action.ArgsStr) ? "{}" : action.ArgsStr)
                .Replace("$action.Url$", action.Url);
        }

        private string GetActionTemplate(RainbowAction action, TypeScriptServiceType type)
        {
            switch (action.Method)
            {
                case "get":
                    return string.IsNullOrEmpty(action.ArgsStr)
                        ? GetTemplate($"{type}.ServiceMethods.GetMethod")
                        : GetTemplate($"{type}.ServiceMethods.GetMethodWithArgument");
                case "post":
                    return GetTemplate($"{type}.ServiceMethods.PostMethod");
                case "put":
                    return GetTemplate($"{type}.ServiceMethods.PutMethod");
                case "delete":
                    return GetTemplate($"{type}.ServiceMethods.DeleteMethod");
            }

            return null;
        }


        private RainbowAction GetRainbowAction(MethodInfo method)
        {
            var routeAttr = method.GetCustomAttribute<RouteAttribute>();
            routeAttr = routeAttr ?? new RouteAttribute("");
            var methodName = method.Name;

            if (Regex.IsMatch(methodName, "{")) methodName = Regex.Match(methodName, @"(.*){").Groups[1].Value;

            var description = method.GetCustomAttribute<DisplayAttribute>()?.Name ?? methodName;
            var url = $"{routeAttr.Template}".Replace("{", "${");

            var returnStr = method.GetReturnTypeString();
            var httpMethod = method.GetHttpMethod();
            // todo

            return new RainbowAction
            {
                Name = methodName,
                Description = description,
                ReturnStr = returnStr,
                ArgParamsStr = method.GetArgumentParamString(),
                ArgsStr = method.GetArgumentsString(),
                Method = httpMethod,
                Url = url,
                IsBaseType = !method.IsClassArguments()
            };
        }

        private static string GetTemplate(string fileName)
        {
            var resName = $"Rainbow.TypeScript.Template.{fileName}.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }

        public void GenerateTypeScriptContracts(ContractSetting setting)
        {
            //}
            //public void GenerateTypeScriptContracts(Assembly assembly, string outputPath, IEnumerable<Type> extTypes)
            //{
            var models = setting.Assembly.GetTypes();

            var generator = new TypeScriptFluent()
                .WithConvertor<Guid>(c => "string")
                .WithConvertor<string>(c => "string")
                .WithConvertor<bool>(c => "boolean");

            generator.ModelBuilder.Add<DataType>();

            foreach (var model in models) generator.ModelBuilder.Add(model);

            foreach (var extType in setting.ExtTypes) generator.ModelBuilder.Add(extType);

            if (!Directory.Exists(setting.OutputPath)) Directory.CreateDirectory(setting.OutputPath);

            //Generate enums
            var tsEnumDefinitions = generator.Generate(TsGeneratorOutput.Enums);
            var tsEnumStringBuilder = new StringBuilder();
            tsEnumStringBuilder.AppendLine("// tslint:disable:no-namespace");
            tsEnumStringBuilder.Append(tsEnumDefinitions);

            File.WriteAllText(Path.Combine(setting.OutputPath, "enums.ts"), tsEnumStringBuilder.ToString());
            //Generate interface definitions for all classes

            var tsClassStringBuilder = new StringBuilder();
            tsClassStringBuilder.AppendLine("// tslint:disable:no-empty-interface");
            tsClassStringBuilder.AppendLine("// tslint:disable:no-namespace");
            var tsClassDefinitions = generator.Generate(TsGeneratorOutput.Properties | TsGeneratorOutput.Fields);

            tsClassStringBuilder.AppendLine(tsClassDefinitions);
            File.WriteAllText(Path.Combine(setting.OutputPath, "classes.d.ts"), tsClassStringBuilder.ToString());
        }
    }
}