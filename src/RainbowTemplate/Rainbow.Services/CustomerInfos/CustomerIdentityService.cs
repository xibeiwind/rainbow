using Rainbow.Common.Configs;
using Rainbow.ViewModels.CustomerInfos;
using System;
using Yunyong.Cache.Abstractions;
using Yunyong.Core;

namespace Rainbow.Services.CustomerInfos
{
    public class CustomerIdentityService : ICustomerIdentityService
    {
        public CustomerIdentityService(
                ICacheService<CustomerLoginTrackVM> service,
                TokenSettings settings
            )
        {
            Service = service;
            Settings = settings;
        }

        private ICacheService<CustomerLoginTrackVM> Service { get; }
        private TokenSettings Settings { get; }

        #region Implementation of ICustomerIdentityService

        public bool IsLogin(Guid customerId, Guid signId)
        {
            var result = Service.GetOrDefault<CustomerLoginTrackVM>(LoginTrackId(customerId));
            if (result?.SignId != signId)
                return false;
            return true;
        }

        public CustomerLoginTrackVM LoginTrack(Guid customerId)
        {
            var signId = GuidUtil.NewSequentialId();

            var vm = new CustomerLoginTrackVM
            {
                CustomerId = customerId,
                SignId = signId,
                ExpiresTime = DateTime.Now.Add(Settings.TokenValidity)
            };
            Service.Set(LoginTrackId(customerId), vm, Settings.TokenValidity);
            return vm;
        }

        public void ExtendSignTime(Guid customerId)
        {
            var result = Service.GetOrDefault<CustomerLoginTrackVM>(LoginTrackId(customerId));
            result.ExpiresTime = DateTime.Now.Add(Settings.TokenValidity);
            Service.Set(LoginTrackId(customerId), result, Settings.TokenValidity);
        }

        public void Logout(Guid customerId)
        {
            Service.Remove(LoginTrackId(customerId));
        }

        private static string LoginTrackId(Guid customerId)
        {
            return $"LoginTrack:{customerId}";
        }

        #endregion
    }
}