using System;
using Rainbow.Common.Configs;
using Rainbow.ViewModels.Users;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public class IdentityService : IIdentityService
    {
        public IdentityService(ICacheService<UserLoginTrackVM> service, TokenSettings settings)
        {
            Service = service;
            Settings = settings;
        }

        private ICacheService<UserLoginTrackVM> Service { get; }
        private TokenSettings Settings { get; }

        public bool IsLogin(Guid userId, Guid signId)
        {
            var result = Service.GetOrDefault<UserLoginTrackVM>(LoginTrackId(userId));
            if (result?.SignId != signId)
                return false;
            return true;
        }

        public UserLoginTrackVM Login(Guid userId)
        {
            var signId = GuidUtil.NewSequentialId();

            var vm = new UserLoginTrackVM { UserId = userId, SignId = signId, ExpiresTime = DateTime.Now.AddDays(1) };
            Service.Set(LoginTrackId(userId), vm, Settings.TokenValidity);
            return vm;
        }

        public void ExtendSignTime(Guid userId)
        {
            var result = Service.GetOrDefault<UserLoginTrackVM>(LoginTrackId(userId));
            result.ExpiresTime = DateTime.Now.AddDays(1);
            Service.Set(LoginTrackId(userId), result, Settings.TokenValidity);
        }

        public void Logout(Guid userId)
        {
            Service.Remove(LoginTrackId(userId));
        }

        private static string LoginTrackId(Guid userId)
        {
            return $"LoginTrack:{userId}";
        }
    }
}