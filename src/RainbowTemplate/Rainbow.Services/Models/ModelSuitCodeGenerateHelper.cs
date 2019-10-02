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
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.ViewModels.ClientModules;
using Rainbow.ViewModels.Models;

namespace Rainbow.Services.Models
{
    public class ModelSuitCodeGenerateHelper
    {
        public ModelSuitCodeGenerateHelper(CreateModelSuitApplyVM suitApplyVm, ProjectSettings settings)
        {
            Settings = settings;

            if (suitApplyVm != null)
            {
                SuitApplyVM = suitApplyVm;

                ModelType = GetModelType(suitApplyVm.ModelFullName);
                ModelDisplayName = ModelType.GetCustomAttribute<DisplayAttribute>()?.Name ?? ModelType.Name;
            }
        }

        private Type ModelType { get; }
        private string ModelDisplayName { get; }
        private CreateModelSuitApplyVM SuitApplyVM { get; }
        public ProjectSettings Settings { get; }

        public async Task UpdateAppRoutingModule(IEnumerable<ClientModuleVM> items)
        {
            var pathRoot = Path.Combine(Settings.PlatformWebRoot, @"ClientApp\src\app");
            //using (var conn = GetConnection())
            {
                var routes = string.Join("\r\n  ", items.Select(GetRoutSetting));

                var appRoutingTemplate = GetTemplate("NgComponent.AppRoutingModuleScript");
                var result = appRoutingTemplate.Replace("$ModuleRoutes$", routes);

                File.WriteAllText(Path.Combine(pathRoot, "app-routing.module.ts"), result, Encoding.UTF8);

                foreach (var item in items)
                    if (!File.Exists(Path.Combine(pathRoot, item.Name.SnakeCase("-"))))
                    {
                        var cmd = $"ng g m {item.Name} --routing --force";
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

                        var modelSnakeName = item.Name.SnakeCase("-");

                        {
                            var template = GetTemplate("NgComponent.ListModuleScript");
                            var filePath = Path.Combine(pathRoot, modelSnakeName, $@"{modelSnakeName}.module.ts");
                            template = template.Replace("$ModelName$", item.Name)
                                .Replace("$ModelSnackName$", modelSnakeName);

                            File.WriteAllText(filePath, template);
                        }
                    }
            }
        }


        private string GetRoutSetting(ClientModuleVM module)
        {
            var template =
                "{ path: '$Path$', loadChildren: '$LoadChildren$', data: { title: '$Title$', customLayout: $CustomLayout$ } },";

            var snakeName = module.Name.SnakeCase("-");

            return template.Replace("$Path$", module.Path)
                .Replace("$Title$", module.Title)
                .Replace("$Name$", module.Name)
                .Replace("$CustomLayout$", module.IsCustomLayout.ToString().ToLower())
                .Replace("$LoadChildren$", $"./{snakeName}/{snakeName}.module#{module.Name}Module");
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

                    template = template.Replace("$PropertyList$",
                        string.Join("", GetVMFieldStrings(propList, item.Type == VMType.Query)));
                }
                else
                {
                    template = template.Replace("$PropertyList$", "");
                }

                template = template.Replace("$UsingNamespace$", @"");

                File.WriteAllText(Path.Combine(path, $"{item.Name}.auto.cs"), template, Encoding.UTF8);
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
            foreach (var prop in propList) yield return GetVMFieldString(prop, isQueryVM);
        }

        private string GetTypeName(Type type)
        {
            var typeDic = new Dictionary<Type, string>
            {
                {typeof(string), "string"},
                {typeof(int), "int"},
                {typeof(Guid), "Guid"},
                {typeof(long), "long"},
                {typeof(double), "double"},
                {typeof(decimal), "decimal"},
                {typeof(float), "float"},
                {typeof(bool), "bool"}
            };

            if (typeDic.TryGetValue(type, out var name)) return name;

            return type.Name;
        }

        private string GetVMFieldString(PropertyInfo prop, bool isQueryVM = false)
        {
            var displayName = prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name;
            var required = isQueryVM ? "" : prop.GetCustomAttribute<RequiredAttribute>() != null ? ",Required" : "";

            var queryColumn = "";

            var dataTypeAttribute = prop.GetCustomAttribute<DataTypeAttribute>();
            var dataTypeStr = dataTypeAttribute != null && dataTypeAttribute.DataType != DataType.Custom
                ? $@"
        [DataType(DataType.{dataTypeAttribute.DataType})]"
                : "";

            var returnType = GetTypeName(prop.PropertyType); // prop.PropertyType.Name;
            if (prop.PropertyType.IsGenericType)
                returnType = prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    //? $"{prop.PropertyType.GetGenericArguments()[0].Name}?"
                    //: $"{Regex.Match(prop.PropertyType.Name, @"([0-9a-zA-Z]+)`").Groups[1].Value}<{string.Join(",", prop.PropertyType.GetGenericArguments().Select(b => b.Name))}>";
                    ? $"{GetTypeName(prop.PropertyType.GetGenericArguments()[0])}"
                    : $"{Regex.Match(prop.PropertyType.Name, @"([0-9a-zA-Z]+)`").Groups[1].Value}<{string.Join(",", prop.PropertyType.GetGenericArguments().Select(GetTypeName))}>";
            if (isQueryVM)
            {
                if (prop.PropertyType.IsEnum)
                    returnType = $"{GetTypeName(prop.PropertyType)}?";

                else if (!prop.PropertyType.IsClass) returnType = $"{GetTypeName(prop.PropertyType)}?";

                if (prop.PropertyType == typeof(string))
                    queryColumn = $@"
        [QueryColumn(""{prop.Name}"", CompareEnum.Like)]";
                // [QueryColumn("Name", CompareEnum.Like)]
            }


            var template = @"
        /// <summary>
        ///     $displayName$
        /// </summary>
        [ListDisplay(Name = ""$displayName$"")$required$]$dataTypeStr$$QueryColumn$
        public $ReturnType$ $PropertyName$ { get; set; }
";
            return template.Replace("$displayName$", displayName)
                .Replace("$required$", required)
                .Replace("$dataTypeStr$", dataTypeStr)
                .Replace("$ReturnType$", returnType)
                .Replace("$PropertyName$", prop.Name)
                .Replace("$QueryColumn$", queryColumn);
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
        [ListDisplay(Name=""{item.DisplayName}"")]
        Task<AsyncTaskTResult<Guid>> {item.ActionName}Async({item.Name} vm);";
                case VMType.Query:
                    return $@"
        /// <summary>
        ///     {item.DisplayName}列表（分页）
        /// </summary>
        [ListDisplay(Name = ""{item.DisplayName}列表（分页）"")]
        Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async({item.Name} option);";
                case VMType.ListDisplay:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [ListDisplay(Name = ""获取{item.DisplayName}"")]
        Task < {item.Name} > Get{item.ActionName}Async(Guid id);

        /// <summary>
        ///     获取{item.DisplayName}列表
        /// </summary>
        [ListDisplay(Name = ""获取{item.DisplayName}列表"")]
        Task < List<{item.Name}> > Get{item.ActionName}ListAsync();";
                case VMType.Delete:
                    return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [ListDisplay(Name=""删除{ModelDisplayName}"")]
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
        [ListDisplay(Name=""删除{ModelDisplayName}"")]
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
                if (SuitApplyVM.EnableDelete) methodList.Add(GetServiceInterfaceDeleteMethod());

                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"I{SuitApplyVM.ModelName}ActionService.auto.cs"), template,
                    Encoding.UTF8);
            }

            items = SuitApplyVM.Items.Where(a => a.Type == VMType.ListDisplay || a.Type == VMType.Query);
            if (items.Any())
            {
                var template = GetTemplate("IQueryService");

                template = template.Replace("$RootNamespace$", Settings.SolutionNamespace)
                    .Replace("$FolderName$", SuitApplyVM.FolderName)
                    .Replace("$Model$", $"{SuitApplyVM.ModelName}")
                    .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Action Service");


                var displayVMs = items.Where(a => a.Type == VMType.ListDisplay);

                var methodList = displayVMs.Select(GetServiceInterfaceMethod).ToList();

                var queryVMs = items.Where(a => a.Type == VMType.Query);

                methodList.AddRange(queryVMs.Select(GetServiceInterfaceMethod));
                template = template.Replace("$ActionMethods$", string.Join("", methodList));

                File.WriteAllText(Path.Combine(path, $"I{SuitApplyVM.ModelName}QueryService.auto.cs"), template,
                    Encoding.UTF8);
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
        [ListDisplay(Name=""{item.DisplayName}"")]
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
        [ListDisplay(Name=""{item.DisplayName}"")]
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
        [ListDisplay(Name = ""{item.DisplayName}列表（分页）"")]
        public async Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async({item.Name} option)
        {{
            using (var conn = GetConnection())
            {{
                return await conn.PagingListAsync<{SuitApplyVM.ModelName}, {SuitApplyVM.ModelName}VM>(option);
            }}
        }}";
                case VMType.ListDisplay:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [ListDisplay(Name = ""获取{item.DisplayName}"")]
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
        [ListDisplay(Name = ""获取{item.DisplayName}列表"")]
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
        [ListDisplay(Name=""删除{ModelDisplayName}"")]
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

                File.WriteAllText(Path.Combine(path, $"{SuitApplyVM.ModelName}ActionService.auto.cs"), template,
                    Encoding.UTF8);
            }

            items = SuitApplyVM.Items.Where(a => a.Type == VMType.ListDisplay || a.Type == VMType.Query);
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

                File.WriteAllText(Path.Combine(path, $"{SuitApplyVM.ModelName}QueryService.auto.cs"), template,
                    Encoding.UTF8);
            }
        }

        private string GetServiceDeleteMethod()
        {
            return $@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [ListDisplay(Name=""删除{ModelDisplayName}"")]
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


            File.WriteAllText(Path.Combine(Settings.HostingStartupsRoot, $"{SuitApplyVM.ModelName}Startup.cs"),
                template,
                Encoding.UTF8);
        }

        private string GetControllerMethod(CreateViewModelApplyVM item)
        {
            var template = "";
            switch (item.Type)
            {
                case VMType.Create:
                    template = GetTemplate("ControllerActions.CreateAction");
                    break;
                case VMType.Update:
                    template = GetTemplate("ControllerActions.UpdateAction");
                    break;
                case VMType.Query:
                    template = GetTemplate("ControllerActions.QueryAction");
                    break;
                case VMType.ListDisplay:
                    template = GetTemplate("ControllerActions.DisplayAndListAction");
                    break;
                case VMType.Delete:
                    template = GetTemplate("ControllerActions.DeleteAction");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var actionAuthorize = item.WithAuthorize
                ? item.AuthorizeRoles?.Any() == true
                    ?
                    $"\r\n        [Authorize(Roles=\"{string.Join(",", item.AuthorizeRoles)}\")]"
                    :
                    "\r\n        [Authorize]"
                : "";

            return template
                .Replace("$item.DisplayName$", item.DisplayName)
                .Replace("$item.ActionName$", item.ActionName)
                .Replace("$item.ActionName$", item.ActionName)
                .Replace("$SuitApplyVM.ModelName$", SuitApplyVM.ModelName)
                .Replace("$item.Name$", item.Name)
                .Replace("$ActionAuthorize$", actionAuthorize)
                ;

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
        [ListDisplay(Name=""{item.DisplayName}"")]
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
        [ListDisplay(Name = ""{item.DisplayName}列表（分页）"")]
        [HttpGet]
        [Route(""{item.ActionName}"")]
        [ProducesDefaultResponseType(typeof(PagingList<{SuitApplyVM.ModelName}VM>))]
        public async Task<PagingList<{SuitApplyVM.ModelName}VM>> {item.ActionName}Async([FromQuery]{item.Name} option)
        {{
            return await QueryService.{item.ActionName}Async(option);
        }}
";
                case VMType.ListDisplay:
                    return $@"
        /// <summary>
        ///     获取{item.DisplayName}
        /// </summary>
        [ListDisplay(Name = ""获取{item.DisplayName}"")]
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
        [ListDisplay(Name = ""获取{item.DisplayName}列表"")]
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
                .Replace("$ControllerProjectName$", SuitApplyVM.ControllerProjectName)
                .Replace("$ControllerAuthorize$", GetControllerWithAuthorize()) // SuitApplyVM.ControllerWithAuthorize)
                .Replace("$DisplayName$", $"{SuitApplyVM.ModelName} Controller");

            var methodList = new List<string>();

            methodList.AddRange(SuitApplyVM.Items.Select(GetControllerMethod));

            if (SuitApplyVM.EnableDelete)
                methodList.Add($@"
        /// <summary>
        ///     删除{ModelDisplayName}
        /// </summary>
        [ListDisplay(Name=""删除{ModelDisplayName}"")]
        [HttpDelete]
        [Route(""Delete"")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> DeleteAsync([FromQuery]Delete{SuitApplyVM.ModelName}VM vm)
        {{
            return await ActionService.DeleteAsync(vm);
        }}
");

            template = template.Replace("$ActionMethods$", string.Join("\r\n", methodList));

            File.WriteAllText(Path.Combine(Settings.ControllerRoot, $"{SuitApplyVM.ModelName}Controller.auto.cs"), template,
                Encoding.UTF8);
        }

        private string GetControllerWithAuthorize()
        {
            return SuitApplyVM.ControllerWithAuthorize
                ? SuitApplyVM.AuthorizeRoles.Any()
                    ? $"\r\n    [Authorize(Roles=\"{string.Join(",", SuitApplyVM.AuthorizeRoles)}\")]"
                    : "\r\n    [Authorize]"
                : "";
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
                            template = template.Replace("$ModelName$", SuitApplyVM.NgModuleName)
                                .Replace("$ModelSnackName$", modelSnakeName);

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
                            var filePath = Path.Combine(pathRoot, moduleSnackName, moduleSnackName,
                                $@"{moduleSnackName}.component.html");
                            File.WriteAllText(filePath, template);
                        }
                        {
                            //$ModelName$
                            var template = GetTemplate("NgComponent.ModuleRoutingScript")
                                    .Replace("$ModelName$", SuitApplyVM.NgModuleName)
                                    .Replace("$ModelSnackName$", moduleSnackName)
                                ;
                            var filePath = Path.Combine(pathRoot, moduleSnackName,
                                $@"{moduleSnackName}-routing.module.ts");
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
                        var filePath = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-"), modelSnakeName,
                            $@"{modelSnakeName}.component.html");
                        File.WriteAllText(filePath, template);
                    }
                    {
                        var template = GetTemplate("NgComponent.ListComponentScript");
                        var filePath = Path.Combine(pathRoot, SuitApplyVM.NgModuleName.SnakeCase("-"), modelSnakeName,
                            $@"{modelSnakeName}.component.ts");

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