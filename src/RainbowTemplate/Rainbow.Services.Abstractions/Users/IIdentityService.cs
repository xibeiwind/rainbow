using Rainbow.ViewModels.Users;
using System;

namespace Rainbow.Services.Users
{
    public interface IIdentityService
    {
        // 检查用户是否登陆
        bool IsLogin(Guid userId, Guid signId);

        // 用户登陆，如何避免一个账号多次登陆？
        UserLoginTrackVM Login(Guid userId);

        /// <summary>
        ///     延长Token有效时间
        /// </summary>
        /// <param name="userId"></param>
        void ExtendSignTime(Guid userId);

        // 用户登出
        void Logout(Guid userId);
    }
}