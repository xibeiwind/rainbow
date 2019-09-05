using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Rainbow.TypeScript
{
    internal static class RainbowActionExtensions
    {
        public static string GetReturnTypeString(this MethodInfo method)
        {
            var respTypeAttr = method.GetCustomAttribute<ProducesDefaultResponseTypeAttribute>();
            if (respTypeAttr != null) return respTypeAttr.Type.GetTypeString();


            return "any";
        }

        public static string GetHttpMethod(this MethodInfo method)
        {
            var dic = new Dictionary<Type, string>
            {
                {typeof(HttpGetAttribute), "get"},
                {typeof(HttpPostAttribute), "post"},
                {typeof(HttpPutAttribute), "put"},
                {typeof(HttpDeleteAttribute), "delete"},
                {typeof(HttpPatchAttribute), "patch"}
            };

            foreach (var type in dic.Keys)
                if (method.GetCustomAttribute(type) != null)
                    return dic[type];

            return "get";
        }

        public static string GetTypeString(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (genericType == typeof(IEnumerable<>) || genericType == typeof(List<>))
                    return $@"{type.GenericTypeArguments.FirstOrDefault()?.FullName}[]";


                var name = genericType?.FullName?.Substring(0,
                    genericType.FullName.IndexOf("`", StringComparison.Ordinal));
                return $@"{name}<{
                    string.Join(", ",
                        type.GenericTypeArguments.Select(GetTypeString))}>";
            }

            var dic = new Dictionary<Type, string>
            {
                {typeof(Guid), "string"},
                {typeof(int), "number"},
                {typeof(long), "number"},
                {typeof(float), "number"},
                {typeof(double), "number"},
                {typeof(decimal), "number"},
                {typeof(string), "string"},
                {typeof(bool), "boolean"}
            };
            return dic.TryGetValue(type, out var result) ? result : type.FullName;
        }

        public static string GetArgumentParamString(this MethodInfo method)
        {
            var args = method.GetParameters();

            return string.Join(", ", args.Select(a => $"{a.Name}: {GetTypeString(a.ParameterType)}"));
        }


        public static string GetArgumentsString(this MethodInfo method)
        {
            var args = method.GetParameters();
            return string.Join(", ", args.Select(a => a.Name));
        }

        public static bool IsClassArguments(this MethodInfo method)
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

        public static string GetVueActionTemplate(this RainbowAction action)
        {
            switch (action.Method)
            {
                case "get":
                    return string.IsNullOrEmpty(action.ArgsStr)
                        ? GetTemplate("ServiceMethods.Vue.GetMethod")
                        : GetTemplate("ServiceMethods.Vue.GetMethodWithArgument");
                case "post":
                    return GetTemplate("ServiceMethods.Vue.PostMethod");
                case "put":
                    return GetTemplate("ServiceMethods.Vue.PutMethod");
                case "delete":
                    return GetTemplate("ServiceMethods.Vue.DeleteMethod");
            }

            return null;
        }


        private static string GetTemplate(string fileName)
        {
            var resName = $"Rainbow.TypeScript.Template.{fileName}.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }
    }
}