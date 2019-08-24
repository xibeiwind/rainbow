using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.Models;

namespace Rainbow.Services.Models
{
    public class ModelSuitCodeGenerateHelper
    {
        private Type ModelType { get; }
        private string ModelDisplayName { get; }
        private CreateModelSuitApplyVM SuitApplyVM { get; }
        public ProjectSettings Settings { get; }
        public ModelSuitCodeGenerateHelper(CreateModelSuitApplyVM suitApplyVm, ProjectSettings settings)
        {
            this.SuitApplyVM = suitApplyVm;
            Settings = settings;

            ModelType = GetModelType(suitApplyVm.ModelFullName);

            ModelDisplayName = ModelType.GetCustomAttribute<DisplayAttribute>()?.Name ?? ModelType.Name;
        }

        private Type GetModelType(string modelName)
        {
            return Assembly.Load("Rainbow.Models").GetType(modelName);
        }

        private string GetTemplate(string fileName)
        {
            var resName = $"Rainbow.Services.Templates.{fileName}.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }
        public void CreateViewModels()
        {
            // ViewModelProject root path
            var path = Path.Combine(Settings.ViewModelRoot, SuitApplyVM.FolderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            foreach (var item in SuitApplyVM.Items)
            {
                var template = GetTemplate($"ViewModels.{item.Type}VM");
                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Name$", item.Name)
                    .Replace("$DisplayName$", item.DisplayName)
                    .Replace("$ModelName$", SuitApplyVM.ModelName);

                if (item.Fields.Any())
                {
                    var propList = item.Fields.Select(a => ModelType.GetProperty(a)).ToList();

                    template = template.Replace("$PropertyList$", string.Join("", GetVMFieldStrings(propList, item.Type != VMType.Query)));
                }
                else
                {
                    template = template.Replace("$PropertyList$", "");
                }

                template = template.Replace("$UsingNamespace$", @"");

                File.WriteAllText(Path.Combine(path, $"{item.Name}.cs"), template, Encoding.UTF8);
            }

            if (SuitApplyVM.EnableDelete)
            {
                var template = GetTemplate("ViewModels.DeleteVM");
                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Name$", $"Delete{SuitApplyVM.ModelName}VM")
                    .Replace("$DisplayName$", $"删除{SuitApplyVM.ModelName}")
                    .Replace("$ModelName$", SuitApplyVM.ModelName);

                template = template.Replace("$UsingNamespace$", $@"
using {Settings.SolutionNamespace}.Common;
using {Settings.SolutionNamespace}.Common.Enums;
");

                File.WriteAllText(Path.Combine(path, $"Delete{SuitApplyVM.ModelName}VM.cs"), template, Encoding.UTF8);
            }
        }

        private IEnumerable<string> GetVMFieldStrings(List<PropertyInfo> propList, bool isQueryVM = false)
        {
            foreach (var prop in propList)
            {
                yield return GetVMFieldString(prop, isQueryVM);
            }
        }

        private string GetVMFieldString(PropertyInfo prop, bool isQueryVM=false)
        {
            var displayName = prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name;
            var required =isQueryVM? "": (prop.GetCustomAttribute<RequiredAttribute>() != null ? ",Required" : "");

            var dataTypeAttribute = prop.GetCustomAttribute<DataTypeAttribute>();
            var dataTypeStr = dataTypeAttribute != null ? $@"
        [DataType(DataType.{dataTypeAttribute.DataType})]" : "";

            var returnType = prop.PropertyType.Name;
            if (prop.PropertyType.IsGenericType)
            {
                returnType = prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? $"{prop.PropertyType.GetGenericArguments()[0].Name}?"
                    : $"{Regex.Match(prop.PropertyType.Name, @"([0-9a-zA-Z]+)`").Groups[1].Value}<{string.Join(",", prop.PropertyType.GetGenericArguments().Select(b => b.Name))}>";
            }
            if (isQueryVM)
            {
                if (prop.PropertyType.IsEnum)
                {
                    returnType = $"{prop.PropertyType.Name}?";
                }

                else if (!prop.PropertyType.IsClass)
                {
                    returnType = $"{prop.PropertyType.Name}?";
                }
            }


            var template = @"
        /// <summary>
        ///     $displayName$
        /// </summary>
        [Display(Name = ""$displayName$"")$required$]$dataTypeStr$
        public $ReturnType$ $PropertyName$ {get;set;}
";
            return template.Replace("$displayName$", displayName)
                .Replace("$required$", required)
                .Replace("$dataTypeStr$", dataTypeStr)
                .Replace("$ReturnType$", returnType)
                .Replace("$PropertyName$", prop.Name);
        }

        private string GetServiceInterfaceMethod(CreateViewModelApplyVM item)
        {
            switch (item.Type)
            {
                case VMType.Create:
                case VMType.Update:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}
        /// </summary>
        [Display(Name=""{item.DisplayName}"")]
        Task<AsyncTaskTResult<Guid>> {item.ActionName}Async({item.Name} vm);";
                case VMType.Query:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async({item.Name} option);";
                case VMType.Display:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        Task < {item.Name} > Get{item.ActionName}Async(Guid id);

        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        Task < List<{item.Name}> > Get{item.ActionName}ListAsync();";
                case VMType.Delete:
                    return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [Display(Name=""删除{ModelDisplayName}"")]
        Task<AsyncTaskResult> DeleteAsync(Delete{SuitApplyVM.ModelName}VM vm);";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetServiceInterfaceDeleteMethod()
        {
            return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [Display(Name=""删除{ModelDisplayName}"")]
        Task<AsyncTaskResult> DeleteAsync(Delete{SuitApplyVM.ModelName}VM vm);";
        }

        public void CreateServiceInterfaces()
        {
            var path = Path.Combine(Settings.ServiceInterfaceRoot, SuitApplyVM.FolderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);


            var items = SuitApplyVM.Items.Where(a => a.Type == VMType.Create || a.Type == VMType.Update);
            if (items.Any())
            {
                var template = GetTemplate("IActionService");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                    .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Action Service");

                var methodList = items.Select(GetServiceInterfaceMethod).ToList();
                if (SuitApplyVM.EnableDelete)
                {
                    methodList.Add(GetServiceInterfaceDeleteMethod());
                }

                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"I{SuitApplyVM.ModelName}ActionService.cs"), template, Encoding.UTF8);
            }

            items = SuitApplyVM.Items.Where(a => a.Type == VMType.Display || a.Type == VMType.Query);
            if (items.Any())
            {
                var template = GetTemplate("IQueryService");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                    .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Action Service");


                var displayVMs = items.Where(a => a.Type == VMType.Display);

                var methodList = displayVMs.Select(GetServiceInterfaceMethod).ToList();

                var queryVMs = items.Where(a => a.Type == VMType.Query);

                methodList.AddRange(queryVMs.Select(GetServiceInterfaceMethod));
                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"I{SuitApplyVM.ModelName}QueryService.cs"), template, Encoding.UTF8);
            }
        }

        private string GetServiceMethod(CreateViewModelApplyVM item)
        {
            switch (item.Type)
            {
                case VMType.Create:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}
        /// </summary>
        [Display(Name=""{item.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {item.ActionName}Async({item.Name} vm)
        {{
            using (var conn = GetConnection())
            {{
                var entity = EntityFactory.Create<{SuitApplyVM.ModelName},{item.Name}>(vm);
                // todo:
                await conn.CreateAsync(entity);
                return AsyncTaskResult.Success(entity.Id);
            }}
        }}";
                case VMType.Update:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}
        /// </summary>
        [Display(Name=""{item.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {item.ActionName}Async({item.Name} vm)
        {{
            using (var conn = GetConnection())
            {{
                // todo:
                await conn.UpdateAsync<{SuitApplyVM.ModelName}>(a => a.Id == vm.Id, vm);
                return AsyncTaskResult.Success(vm.Id);
            }}
        }}";
                case VMType.Query:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        public async Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async({item.Name} option)
        {{
            using (var conn = GetConnection())
            {{
                return await conn.PagingListAsync<{SuitApplyVM.ModelName}, {SuitApplyVM.ModelName}VM>(option);
            }}
        }}";
                case VMType.Display:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        public async Task < {item.Name}> Get{item.ActionName}Async(Guid id)
        {{
            using (var conn = GetConnection())
            {{
                return await conn.FirstOrDefaultAsync<{SuitApplyVM.ModelName}, {SuitApplyVM.ModelName}VM>(a => a.Id == id);
            }}
        }}
        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        public async Task <List<{item.Name}>> Get{item.ActionName}ListAsync()
        {{
            using (var conn = GetConnection())
            {{
                return await conn.AllAsync<{SuitApplyVM.ModelName}, {item.Name}>();
            }}
        }}

";
                case VMType.Delete:
                    return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [Display(Name=""删除{ModelDisplayName}"")]
        Task<AsyncTaskResult> DeleteAsync(Delete{SuitApplyVM.ModelName}VM vm);";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void CreateServices()
        {
            var path = Path.Combine(Settings.ServiceRoot, SuitApplyVM.FolderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var items = SuitApplyVM.Items.Where(a => a.Type == VMType.Create || a.Type == VMType.Update);
            if (items.Any())
            {
                var template = GetTemplate("ActionService");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                    .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Action Service");

                var methodList = new List<string>();

                methodList.AddRange(items.Select(GetServiceMethod));

                methodList.Add(GetServiceDeleteMethod());

                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"{SuitApplyVM.ModelName}ActionService.cs"), template, Encoding.UTF8);
            }

            items = SuitApplyVM.Items.Where(a => a.Type == VMType.Display || a.Type == VMType.Query);
            if (items.Any())
            {
                var template = GetTemplate("QueryService");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                    .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Query Service");

                var methodList = new List<string>();
                methodList.AddRange(items.Select(GetServiceMethod));

                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"{SuitApplyVM.ModelName}QueryService.cs"), template, Encoding.UTF8);
            }
        }

        private string GetServiceDeleteMethod()
        {
            return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [Display(Name=""删除{ModelDisplayName}"")]
        public async Task<AsyncTaskResult> DeleteAsync(Delete{SuitApplyVM.ModelName}VM vm)
        {{
            using (var conn = GetConnection())
            {{
                await conn.DeleteAsync<{SuitApplyVM.ModelName}>(a => a.Id == vm.Id);
                return AsyncTaskResult.Success();
            }}
        }}
";
        }

        public void CreateHostingStartup()
        {
            var template = GetTemplate("HostingStartup");
            template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                .Replace("$FolderName$", SuitApplyVM.FolderName)
                .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Hosting Startup");


            File.WriteAllText(Path.Combine(Settings.HostingStartupsRoot, $"{SuitApplyVM.ModelName}Startup.cs"), template,
                Encoding.UTF8);
        }

        private string GetControllerMethod(CreateViewModelApplyVM item)
        {
            switch (item.Type)
            {
                case VMType.Create:
                case VMType.Update:
                    var httpMethod = item.Type == VMType.Create ? "HttpPost" : "HttpPut";
                    return $@"
        /// <summary>
        ///     {item.DisplayName}
        /// </summary>
        [{httpMethod}]
        [Route(""{item.ActionName}"")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<Guid>))]
        [Display(Name=""{item.DisplayName}"")]
        public async Task<AsyncTaskTResult<Guid>> {item.ActionName}Async([FromBody]{item.Name} vm)
        {{
            return await ActionService.{item.ActionName}Async(vm);
        }}
";
                case VMType.Query:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [Display(Name = ""{item.DisplayName}列表（分页）"")]
        [HttpGet]
        [Route(""{item.ActionName}"")]
        [ProducesDefaultResponseType(typeof(PagingList<{SuitApplyVM.ModelName}VM>))]
        public async Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async([FromQuery]{item.Name} option)
        {{
            return await QueryService.{item.ActionName}Async(option);
        }}
";
                case VMType.Display:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}"")]
        [HttpGet]
        [Route(""{item.ActionName}"")]
        [ProducesDefaultResponseType(typeof({item.Name}))]
        public async Task < {item.Name} > Get{item.ActionName}Async(Guid id)
        {{
            return await QueryService.Get{item.ActionName}Async(id);
        }}

        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [Display(Name = ""获取{item.DisplayName}列表"")]
        [HttpGet]
        [Route(""{item.ActionName}List"")]
        [ProducesDefaultResponseType(typeof(List<{item.Name}>))]
        public async Task < List<{item.Name}> > Get{item.ActionName}ListAsync()
        {{
            return await QueryService.Get{item.ActionName}ListAsync();
        }}
";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void CreateControllers()
        {
            var template = GetTemplate("Controller");

            template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                .Replace("$FolderName$", SuitApplyVM.FolderName)
                .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Controller");

            var methodList = new List<string>();

            methodList.AddRange(SuitApplyVM.Items.Select(GetControllerMethod));

            if (SuitApplyVM.EnableDelete)
                methodList.Add($@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [Display(Name=""删除{ModelDisplayName}"")]
        [HttpDelete]
        [Route(""Delete"")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]Delete{SuitApplyVM.ModelName}VM vm)
        {{
            return await ActionService.DeleteAsync(vm);
        }}
");

            template = template.Replace("$ActionMethods$", string.Join("", methodList));

            File.WriteAllText(Path.Combine(Settings.ControllerRoot, $"{SuitApplyVM.ModelName}Controller.cs"), template,
                Encoding.UTF8);
        }

        public void CreateNgModuleComponent()
        {
            var pathRoot = Path.Combine(Settings.PlatformWebRoot, @"ClientApp\src\app");
            {
                Console.WriteLine(pathRoot);

                if (!Directory.Exists(Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-"))))
                {
                    {
                        var cmd = $"ng g m {SuitApplyVM.NgModuleName} --routing --force";
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
                                CreateNoWindow = true
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

                        var modelSnakeName = SuitApplyVM.NgModuleName.SnakeCase("-");

                        {
                            var template = GetTemplate("NgComponent.ListModuleScript");
                            var filePath = Path.Combine(pathRoot, modelSnakeName, $@"{modelSnakeName}.module.ts");
                            template = template.Replace("$ModelName$", SuitApplyVM.NgModuleName).Replace("$ModelSnackName$", modelSnakeName);

                            File.WriteAllText(filePath, template);
                        }
                    }
                    {
                        var cmd = $"ng g c {SuitApplyVM.NgModuleName} -force";
                        Console.WriteLine(pathRoot);
                        Console.WriteLine(cmd);

                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "cmd.exe",
                                WorkingDirectory = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-")),
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                CreateNoWindow = true
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

                        var moduleSnackName = SuitApplyVM.NgModuleName.SnakeCase("-");
                        {
                            var template = GetTemplate("NgComponent.ModuleComponentHtml");
                            var filePath = Path.Combine(pathRoot, moduleSnackName, moduleSnackName, $@"{moduleSnackName}.component.html");
                            File.WriteAllText(filePath, template);
                        }
                        {
                            //$ModelName$
                            var template = GetTemplate("NgComponent.ModuleRoutingScript")
                                .Replace("$ModelName$", SuitApplyVM.NgModuleName)
                                .Replace("$ModelSnackName$", moduleSnackName)
                                ;
                            var filePath = Path.Combine(pathRoot, moduleSnackName, $@"{moduleSnackName}-routing.module.ts");
                            File.WriteAllText(filePath, template);
                        }
                    }

                }
            }
            {
                var cmd = $"ng g c {SuitApplyVM.ModelName} -force";
                Console.WriteLine(pathRoot);
                Console.WriteLine(cmd);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        WorkingDirectory = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-")),
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
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


                if (SuitApplyVM.IsNgModelListComponent)
                {
                    var modelSnakeName = SuitApplyVM.ModelName.SnakeCase("-");
                    {
                        var template = GetTemplate("NgComponent.ListComponentHtml");
                        var filePath = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-"), modelSnakeName, $@"{modelSnakeName}.component.html");
                        File.WriteAllText(filePath, template);
                    }
                    {
                        var template = GetTemplate("NgComponent.ListComponentScript");
                        var filePath = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-"), modelSnakeName, $@"{modelSnakeName}.component.ts");

                        template = template.Replace("$ModelName$", SuitApplyVM.ModelName)
                            .Replace("$ModelSnackName$", modelSnakeName)
                            .Replace("$SolutionNamespace$", Settings.SolutionNamespace);


                        File.WriteAllText(filePath, template);
                    }
                }

            }
        }
    }
}