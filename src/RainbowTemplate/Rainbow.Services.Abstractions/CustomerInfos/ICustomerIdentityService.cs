using Rainbow.ViewModels.CustomerInfos;
using System;

namespace Rainbow.Services.CustomerInfos
{
    public interface ICustomerIdentityService
    {
        // 检查用户是否登陆
        bool IsLogin(Guid customerId, Guid signId);

        // 用户登陆，如何避免一个账号多次登陆？
        CustomerLoginTrackVM LoginTrack(Guid customerId);

        /// <summary>
        ///     延长Token有效时间
        /// </summary>
        void ExtendSignTime(Guid customerId);

        // 用户登出
        void Logout(Guid customerId);
    }
}