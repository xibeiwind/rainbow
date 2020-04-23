using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Rainbow.Services.Users;
using Rainbow.Services.Utils;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Platform.WebAPP.Services
{
    public class CustomerServiceManageService : ServiceBase, ICustomerServiceManageService
    {
        public CustomerServiceManageService(ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus,
            SecurityUtil util)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            Util = util;
        }

        private SecurityUtil Util { get; }

        public async Task InitBuildCustomerService()
        {
            await using var conn = GetConnection();
            foreach (var index in Enumerable.Range(1, 10))
            {
                var tmp = EntityFactory.Create<UserInfo>();
                tmp.Phone = $"1890000{index:0000}";
                tmp.PasswordHash = Util.Encoding($"RainbowUser@{index:0000}");
                tmp.IsActive = true;
                tmp.Name = $"RainbowUser:{index:0000}";

                if (!await conn.ExistAsync<UserInfo>(a => a.Phone == tmp.Phone))
                {
                    await conn.CreateAsync(tmp);

                    var roles = (await conn.AllAsync<RoleInfo>()).ToDictionary(a => a.RoleType);

                    var userRole = new UserRole
                    {
                        UserId = tmp.Id,
                        RoleId = roles[UserRoleType.CustomerService].Id
                    };

                    await conn.CreateAsync(userRole);
                }
            }

            {
                var tmp = EntityFactory.Create<UserInfo>();
                tmp.Phone = $"18900009999";
                tmp.PasswordHash = Util.Encoding($"RainbowUser@9999");
                tmp.IsActive = true;
                tmp.Name = $"SysAdmin";

                if (!await conn.ExistAsync<UserInfo>(a => a.Phone == tmp.Phone))
                {
                    await conn.CreateAsync(tmp);

                    var roles = (await conn.AllAsync<RoleInfo>()).ToDictionary(a => a.RoleType);

                    var userRole = new UserRole
                    {
                        UserId = tmp.Id,
                        RoleId = roles[UserRoleType.SysAdmin].Id
                    };

                    await conn.CreateAsync(userRole);
                }
            }
        }
    }
}