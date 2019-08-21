using System;
using System.Threading.Tasks;
using Rainbow.ViewModels.Users;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public interface ICustomerServiceManageService
    {
        Task InitBuildCustomerService();
        Task<AsyncTaskTResult<LoginResultVM>> Login(LoginVM vm);

        Task<AsyncTaskTResult<CustomerServiceVM>> GetCustomerService(Guid id);

        Task<bool> IsCustomerService(Guid id);
        Task<bool> IsCustomerService(string phone);
        Task<AsyncTaskTResult<bool>> Logout(Guid id);
    }
}