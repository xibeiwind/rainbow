using Rainbow.ViewModels.CustomerInfos;
using System;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Services.CustomerInfos
{
    public interface ICustomerAccountService
    {
        /// <summary>
        ///     检查用户是否登陆
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="signId"></param>
        /// <returns></returns>
        Task<bool> IsLogin(Guid customerId, Guid signId);

        /// <summary>
        ///     用户登陆
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<WechatLoginResultVM> Login(WechatLoginVM vm);

        /// <summary>
        ///     用户登出
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        AsyncTaskTResult<bool> Logout(Guid customerId);
    }
}