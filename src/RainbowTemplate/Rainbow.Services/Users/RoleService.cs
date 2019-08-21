using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        public async Task Init()
        {
            var roleType = typeof(UserRoleType);
            var fields = roleType.GetFields(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);

            using (var conn = GetConnection())
            {
                foreach (var field in fields)
                    if (!await conn.ExistAsync<RoleInfo>(a => a.Name == field.Name))
                    {
                        var role = EntityFactory.Create<RoleInfo>();
                        role.Name = field.Name;
                        role.Description = field.GetCustomAttribute<DisplayAttribute>()?.Name ?? field.Name;
                        role.RoleType = (UserRoleType) field.GetValue(null);
                        await conn.CreateAsync(role);
                    }
            }
        }
    }
}