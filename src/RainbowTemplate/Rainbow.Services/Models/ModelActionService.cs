using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Rainbow.ViewModels.ClientModules;
using Rainbow.ViewModels.Models;

using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Models
{
    public class ModelActionService : ServiceBase, IModelActionService
    {
        public ModelActionService(
                ProjectSettings settings,
                ConnectionSettings connectionSettings,
                IConnectionFactory connectionFactory,
                ILoggerFactory loggerFactory,
                IEventBus eventBus
            )
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            Settings = settings;
        }

        public ProjectSettings Settings { get; }

        public async Task<AsyncTaskTResult<bool>> CreateUpdateFiles(CreateModelSuitApplyVM vm)
        {
            var helper = new ModelSuitCodeGenerateHelper(vm, Settings);

            if (vm.GenerateVM) helper.CreateViewModels();
            //CreateViewModels();
            if (vm.GenerateService)
            {
                helper.CreateServiceInterfaces();
                helper.CreateServices();
                helper.CreateHostingStartup();
            }

            if (vm.GenerateController) helper.CreateControllers();

            if (vm.GenerateNgModuleComponent) helper.CreateNgModuleComponent();

            if (vm.UpdateTsServices) await RegenerateTsCode();


            return AsyncTaskResult.Success(true);
        }

        public async Task<AsyncTaskTResult<bool>> UpdateAppRoutingModule()
        {
            await using var conn = GetConnection();
            var items = await conn.AllAsync<ClientModule, ClientModuleVM>();

            var helper = new ModelSuitCodeGenerateHelper(null, Settings);

            await helper.UpdateAppRoutingModule(items);
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
                    CreateNoWindow = true
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
            return typeof(UserInfo).Assembly.GetType(modelName);
        }

        private string GetVMTemplate(VMType type)
        {
            var resName = $"Rainbow.Services.Templates.ViewModels.{type}VM.txt";
            var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resName));
            return reader.ReadToEnd();
        }
    }
}