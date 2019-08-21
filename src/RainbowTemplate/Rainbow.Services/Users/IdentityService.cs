using System;
using System.Collections.Generic;
using Yunyong.Core;

namespace Rainbow.Services.Users
{
    public class IdentityService : IIdentityService
    {
        private Dictionary<Guid, (Guid signId, DateTime time)> IdentityDic { get; } =
            new Dictionary<Guid, (Guid, DateTime)>();

        public bool IsLogin(Guid userId, Guid signId)
        {
            if (IdentityDic.ContainsKey(userId))
            {
                var (id, time) = IdentityDic[userId];
                if (time > DateTime.Now && id == signId) return true;
            }

            return false;
        }

        public (Guid, DateTime) Login(Guid userId)
        {
            var signId = GuidUtil.NewSequentialId();

            IdentityDic[userId] = (signId, DateTime.Now.AddDays(1));

            return IdentityDic[userId];
        }

        public void ExtendSignTime(Guid userId)
        {
            if (IdentityDic.ContainsKey(userId))
            {
                var (signId, time) = IdentityDic[userId];
                IdentityDic[userId] = (signId, DateTime.Now.AddDays(1));
            }
        }

        public void Logout(Guid userId)
        {
            IdentityDic.Remove(userId);
        }
    }
}