using System;
using Rainbow.ViewModels.Users;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public class IdentityService : IIdentityService
    {
        public IdentityService(ICacheService<UserLoginTrackVM> service)
        {
            Service = service;
        }

        private ICacheService<UserLoginTrackVM> Service { get; }

        public bool IsLogin(Guid userId, Guid signId)
        {
            var result = Service.GetOrDefault<UserLoginTrackVM>($"LoginTrack:{userId}");
            if (result?.SignId != signId)
                return false;
            return true;
        }

        public UserLoginTrackVM Login(Guid userId)
        {
            var signId = GuidUtil.NewSequentialId();

            var vm = new UserLoginTrackVM
            {
                UserId = userId,
                SignId = signId,
                ExpiresTime = DateTime.Now.AddDays(1)
            };
            Service.Set($"LoginTrack:{userId}", vm, TimeSpan.FromDays(1));
            return vm;
        }

        public void ExtendSignTime(Guid userId)
        {
            var result = Service.GetOrDefault<UserLoginTrackVM>($"LoginTrack:{userId}");
            result.ExpiresTime = DateTime.Now.AddDays(1);
            Service.Set($"LoginTrack:{userId}", result, TimeSpan.FromDays(1));
        }

        public void Logout(Guid userId)
        {
            Service.Remove($"LoginTrack:{userId}");
        }
    }
}