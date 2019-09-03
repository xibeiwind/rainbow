using System.IO;
using System.Text;
using Rainbow.Common;
using Rainbow.ViewModels.Services;
using Yunyong.Core;

namespace Rainbow.Services.Services
{
    public class ServiceActionService : IServiceActionService
    {
        private ProjectSettings Settings { get; }

        public ServiceActionService(ProjectSettings settings)
        {
            Settings = settings;
        }


        public AsyncTaskTResult<ServiceVM> UpdateAsync(UpdateServiceVM vm)
        {
            var serviceDir = Settings.ServiceRoot;
            if (!string.IsNullOrEmpty(vm.Folder))
            {
                serviceDir = Path.Combine(Settings.ServiceRoot, vm.Folder);
            }
            if (!Directory.Exists(serviceDir))
            {
                Directory.CreateDirectory(serviceDir);
            }

            var helper = new ServiceGenerateHelper();
            var code = helper.GetServiceString(vm);

            File.WriteAllText(Path.Combine(serviceDir, $"{vm.Name}.cs"), code, Encoding.UTF8);

            return null;
        }

        public AsyncTaskTResult<bool> DeleteAsync(string serviceName)
        {
            return null;
        }
    }
}