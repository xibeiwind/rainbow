using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Rainbow.ViewModels.Models;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.Services.Models
{
    public class ModelActionService : ServiceBase, IModelActionService
    {
        public ModelActionService(ProjectSettings settings, ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            Settings = settings;
        }

        public ProjectSettings Settings { get; }

        public async Task<AsyncTaskTResult<bool>> CreateUpdateFiles(CreateModelSuitApplyVM vm)
        {
            Type modelType = GetModelType(vm.ModelFullName);

            var modelDisplayName = modelType.GetCustomAttribute<DisplayAttribute>()?.Name ?? modelType.Name;

            void CreateViewModels()
            {
                // ViewModelProject root path
                var path = Path.Combine(Settings.ViewModelRoot, vm.FolderName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                foreach (var item in vm.Items)
                {
                    var template = GetVMTemplate(item.Type);
                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Name$", item.Name)
                        .Replace("$DisplayName$", item.DisplayName);

                    if (item.Fields.Any())
                    {
                        var propList = item.Fields.Select(a => modelType.GetProperty(a)).ToList();

                        template = template.Replace("$ProperyList$", string.Join("", GetVMFieldStrings(propList)));
                    }
                    else
                    {
                        template = template.Replace("$ProperyList$", "");
                    }

                    template = template.Replace("$UsingNamespace$", $@"
using {Settings.SolutionNamespace}.Common;
using {Settings.SolutionNamespace}.Common.Enums;
");

                    File.WriteAllText(Path.Combine(path, $"{item.Name}.cs"), template, Encoding.UTF8);
                }

                if (vm.EnableDelete)
                {
                    var template = GetVMDeleteTemplate();
                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Name$", $"Delete{vm.ModelName}VM")
                        .Replace("$DisplayName$", $"删除{vm.ModelName}");

                    template = template.Replace("$UsingNamespace$", $@"
using {Settings.SolutionNamespace}.Common;
using {Settings.SolutionNamespace}.Common.Enums;
");

                    File.WriteAllText(Path.Combine(path, $"Delete{vm.ModelName}VM.cs"), template, Encoding.UTF8);
                }
            }


            void CreateServiceInterfaces()
            {
                var path = Path.Combine(Settings.ServiceInterfaceRoot, vm.FolderName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);


                var items = vm.Items.Where(a => a.Type == VMType.Create || a.Type == VMType.Update);
                if (items.Any())
                {
                    var template = GetTemplate("IActionService");

                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Model$", $"{vm.ModelName}")
                        .Replace("$DisplayName$", $"{vm.ModelName} Action Service");

                    var methodList = items.Select(a =>
                    {
                        return $@"
        /// <summary>
        ///     {a.DisplayName}
        /// </summary>
        [Display(Name=""{a.DisplayName}"")]
        Task<AsyncTaskTResult<Guid>> {a.ActionName}Async({a.Name} vm);
";
                    }).ToList();
                    methodList.Add($@"
        /// <summary>
        ///     删除{modelDisplayName}
        /// </summary>
        [Display(Name=""删除{modelDisplayName}"")]
        Task<AsyncTaskResult> DeleteAsync(Delete{vm.ModelName}VM vm);
");

                    template = template.Replace("$ActionMethods$", string.Join("", methodList));

                    File.WriteAllText(Path.Combine(path, $"I{vm.ModelName}ActionService.cs"), template, Encoding.UTF8);
                }

                items = vm.Items.Where(a => a.Type == VMType.Display || a.Type == VMType.Query);
                if (items.Any())
                {
                    var template = GetTemplate("IQueryService");

                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Model$", $"{vm.ModelName}")
                        .Replace("$DisplayName$", $"{vm.ModelName} Action Service");

                    var methodList = new List<string>();

                    var displayVMs = items.Where(a => a.Type == VMType.Display);
                    foreach (var item in displayVMs)
                    {
                        methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        Task < {item.Name} > Get{item.ActionName}Async(Guid id);
");
                        methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        Task < List<{item.Name}> > Get{item.ActionName}ListAsync();
");
                    }

                    var queryVMs = items.Where(a => a.Type == VMType.Query);
                    foreach (var item in queryVMs)
                        methodList.Add($@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        Task<PagingList<{vm.ModelName}VM>> {item.ActionName}Async({item.Name} option);
");


                    template = template.Replace("$ActionMethods$", string.Join("", methodList));

                    File.WriteAllText(Path.Combine(path, $"I{vm.ModelName}QueryService.cs"), template, Encoding.UTF8);
                }
            }

            void CreateServices()
            {
                var path = Path.Combine(Settings.ServiceRoot, vm.FolderName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var items = vm.Items.Where(a => a.Type == VMType.Create || a.Type == VMType.Update);
                if (items.Any())
                {
                    var template = GetTemplate("ActionService");

                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Model$", $"{vm.ModelName}")
                        .Replace("$DisplayName$", $"{vm.ModelName} Action Service");

                    var methodList = new List<string>();

                    methodList.AddRange(items.Where(a => a.Type == VMType.Create)
                        .Select(a => $@"
        /// <summary>
        ///     {a.DisplayName}
        /// </summary>
        [Display(Name=""{a.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {a.ActionName}Async({a.Name} vm)
        {{
            using (var conn = GetConnection())
            {{
                var entity = EntityFactory.Create<{vm.ModelName},{a.Name}>(vm);
                // todo:
                await conn.CreateAsync(entity);
                return AsyncTaskResult.Success(entity.Id);
            }}
        }}"));

                    methodList.AddRange(items.Where(item => item.Type == VMType.Update)
                        .Select(a => $@"
        /// <summary>
        ///     {a.DisplayName}
        /// </summary>
        [Display(Name=""{a.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {a.ActionName}Async({a.Name} vm)
        {{
            using (var conn = GetConnection())
            {{
                // todo:
                await conn.UpdateAsync<{vm.ModelName}>(a => a.Id == vm.Id, vm);
                return AsyncTaskResult.Success(vm.Id);
            }}
        }}
"));


                    //                     methodList = items.Select(a => { return $@"
                    //        /// <summary>
                    //        ///     {a.DisplayName}
                    //        /// </summary>
                    //        [Display(Name=""{a.DisplayName}"")]
                    //        public async Task<AsyncTaskTResult<Guid>> {a.ActionName}Async({a.Name} vm)
                    //        {{
                    //            using (var conn = GetConnection())
                    //            {{
                    //                throw new NotImplementedException();
                    //            }}
                    //        }}
                    //"; }).ToList();
                    methodList.Add($@"
        /// <summary>
        ///     删除{modelDisplayName}
        /// </summary>
        [Display(Name=""删除{modelDisplayName}"")]
        public async Task<AsyncTaskResult> DeleteAsync(Delete{vm.ModelName}VM vm)
        {{
            using (var conn = GetConnection())
            {{
                await conn.DeleteAsync<{vm.ModelName}>(a => a.Id == vm.Id);
                return AsyncTaskResult.Success();
            }}
        }}
");

                    template = template.Replace("$ActionMethods$", string.Join("", methodList));

                    File.WriteAllText(Path.Combine(path, $"{vm.ModelName}ActionService.cs"), template, Encoding.UTF8);
                }

                items = vm.Items.Where(a => a.Type == VMType.Display || a.Type == VMType.Query);
                if (items.Any())
                {
                    var template = GetTemplate("QueryService");

                    template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                        .Replace("$FolderName$", vm.FolderName)
                        .Replace("$Model$", $"{vm.ModelName}")
                        .Replace("$DisplayName$", $"{vm.ModelName} Query Service");

                    var methodList = new List<string>();

                    var displayVMs = items.Where(a => a.Type == VMType.Display);
                    foreach (var item in displayVMs)
                    {
                        methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        public async Task < {item.Name}> Get{item.ActionName}Async(Guid id)
        {{
            using (var conn = GetConnection())
            {{
                return await conn.FirstOrDefaultAsync<{vm.ModelName}, {vm.ModelName}VM>(a => a.Id == id);
            }}
        }}
");
                        methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        public async Task <List<{item.Name}>> Get{item.ActionName}ListAsync()
        {{
            using (var conn = GetConnection())
            {{
                return await conn.AllAsync<{vm.ModelName}, {item.Name}>();
            }}
        }}
");
                    }

                    var queryVMs = items.Where(a => a.Type == VMType.Query);
                    foreach (var item in queryVMs)
                        methodList.Add($@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        public async Task<PagingList<{vm.ModelName}VM>> {item.ActionName}Async({item.Name} option)
        {{
            using (var conn = GetConnection())
            {{
                return await conn.PagingListAsync<{vm.ModelName}, {vm.ModelName}VM>(option);
            }}
        }}
");

                    template = template.Replace("$ActionMethods$", string.Join("", methodList));

                    File.WriteAllText(Path.Combine(path, $"{vm.ModelName}QueryService.cs"), template, Encoding.UTF8);
                }
            }

            void CreateHostingStartup()
            {
                var template = GetTemplate("HostingStartup");
                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", vm.FolderName)
                    .Replace("$Model$", $"{vm.ModelName}")
                    .Replace("$DisplayName$", $"{vm.ModelName} Hosting Startup");


                File.WriteAllText(Path.Combine(Settings.HostingStartupsRoot, $"{vm.ModelName}Startup.cs"), template,
                    Encoding.UTF8);
            }

            void CreateControllers()
            {
                var template = GetTemplate("Controller");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", vm.FolderName)
                    .Replace("$Model$", $"{vm.ModelName}")
                    .Replace("$DisplayName$", $"{vm.ModelName} Controller");

                var methodList = new List<string>();

                var items = vm.Items.Where(a => a.Type == VMType.Display || a.Type == VMType.Query);
                var displayVMs = items.Where(a => a.Type == VMType.Display);
                foreach (var item in displayVMs)
                {
                    methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        [HttpGet]
        [Route(""{item.ActionName}"")]
        [ProducesResponseType(typeof({item.Name}), 200)]
        public async Task < {item.Name} > Get{item.ActionName}Async(Guid id)
        {{
            return await QueryService.Get{item.ActionName}Async(id);
        }}
");
                    methodList.Add($@"
        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        [HttpGet]
        [Route(""{item.ActionName}List"")]
        [ProducesResponseType(typeof(List<{item.Name}>), 200)]
        public async Task < List<{item.Name}> > Get{item.ActionName}ListAsync()
        {{
            return await QueryService.Get{item.ActionName}ListAsync();
        }}
");
                }

                var queryVMs = items.Where(a => a.Type == VMType.Query);
                foreach (var item in queryVMs)
                    methodList.Add($@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        [HttpGet]
        [Route(""{item.ActionName}"")]
        [ProducesResponseType(typeof(PagingList<{vm.ModelName}VM>), 200)]
        public async Task<PagingList<{vm.ModelName}VM>> {item.ActionName}Async([FromQuery]{item.Name} option)
        {{
            return await QueryService.{item.ActionName}Async(option);
        }}
");

                items = vm.Items.Where(a => a.Type == VMType.Create || a.Type == VMType.Update);
                if (items.Any())
                    methodList.AddRange(items.Select(a =>
                    {
                        var httpMethod = a.Type == VMType.Create ? "HttpPost" : "HttpPut";
                        return $@"
        /// <summary>
        ///     {a.DisplayName}
        /// </summary>
        [{httpMethod}]
        [Route(""{a.ActionName}"")]
        [ProducesResponseType(typeof(AsyncTaskTResult<Guid>), 200)]
        [Display(Name=""{a.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {a.ActionName}Async([FromBody]{a.Name} vm)
        {{
            return await ActionService.{a.ActionName}Async(vm);
        }}
";
                    }));

                if (vm.EnableDelete)
                    methodList.Add($@"
        /// <summary>
        ///     删除{modelDisplayName}
        /// </summary>
        [Display(Name=""删除{modelDisplayName}"")]
        [HttpDelete]
        [Route(""Delete"")]
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]Delete{vm.ModelName}VM vm)
        {{
            return await ActionService.DeleteAsync(vm);
        }}
");

                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(Settings.ControllerRoot, $"{vm.ModelName}Controller.cs"), template,
                    Encoding.UTF8);
            }

            void CreateNgModuleComponent()
            {
                var pathRoot = Path.Combine(Settings.PlatformWebRoot, @"ClientApp\src\app");
                {
                    Console.WriteLine(pathRoot);

                    var cmd = $"ng g m {vm.ModelName} --routing --force";
                    Console.WriteLine(cmd);
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",

                            WorkingDirectory = pathRoot,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                        }
                    };
                    process.OutputDataReceived += (sender, args) => { Console.WriteLine(args.Data); };
                    process.ErrorDataReceived += (sender, args) => { Console.Error.WriteLine(args.Data); };

                    process.Start();
                    process.StandardInput.WriteLine(cmd);
                    process.StandardInput.WriteLine("exit");
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    process.Close();
                }
                {
                    var cmd = $"ng g c {vm.ModelName} --force";
                    Console.WriteLine(pathRoot);
                    Console.WriteLine(cmd);

                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            WorkingDirectory = pathRoot,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                        }
                    };
                    process.OutputDataReceived += (sender, args) => { Console.WriteLine(args.Data); };
                    process.ErrorDataReceived += (sender, args) => { Console.Error.WriteLine(args.Data); };

                    process.Start();
                    process.StandardInput.WriteLine(cmd);
                    process.StandardInput.WriteLine("exit");
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    process.Close();
                }
            }


            CreateViewModels();
            if (vm.GenerateService)
            {
                CreateServiceInterfaces();
                CreateServices();
                CreateHostingStartup();
            }

            if (vm.GenerateController) CreateControllers();

            if (vm.GenerateNgModuleComponent) CreateNgModuleComponent();
            if (vm.UpdateTsServices)
            {
                await RegenerateTsCode();
            }

            return AsyncTaskResult.Success(true);
        }
            public async Task<AsyncTaskTResult<bool>> RegenerateTsCode()
            {
                var pathRoot = Path.Combine(Settings.SolutionRoot, @"Rainbow.TypeLiteConsoleApp");

                var cmd = $"run {Settings.SolutionRoot}";
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet.exe",
                        Arguments = cmd,
                        WorkingDirectory = pathRoot,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    }
                };

                process.OutputDataReceived += (sender, args) => { Console.WriteLine(args.Data); };
                process.ErrorDataReceived += (sender, args) => { Console.Error.WriteLine(args.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();

                return AsyncTaskResult.Success(true);
            }

        private string GetVMDeleteTemplate()
        {
            var resName = "Rainbow.Services.Templates.ViewModels.DeleteVM.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }

        private string GetTemplate(string fileName)
        {
            var resName = $"Rainbow.Services.Templates.{fileName}.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }

        private IEnumerable<string> GetVMFieldStrings(List<PropertyInfo> propList)
        {
            return propList.Select(prop =>
            {
                var displayName = prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name;

                var returnType = prop.PropertyType.Name;
                if (prop.PropertyType.IsGenericType)
                {
                    if (prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        returnType = prop.PropertyType.GetGenericArguments()[0].Name;
                    else
                        returnType =
                            $"{Regex.Match(prop.PropertyType.Name, @"([0-9a-zA-Z]+)`").Groups[1].Value}<{string.Join(",", prop.PropertyType.GetGenericArguments().Select(b => b.Name))}>";
                }

                return $@"
        /// <summary>
        ///     {displayName}
        /// </summary>
        [Display(Name = ""{displayName}"")]
        public {returnType} {prop.Name} {{ get; set; }}
";
            });
        }

        private Type GetModelType(string modelName)
        {
            return typeof(User).Assembly.GetType(modelName);
        }

        private string GetVMTemplate(VMType type)
        {
            var resName = $"Rainbow.Services.Templates.ViewModels.{type}VM.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }
    }
}