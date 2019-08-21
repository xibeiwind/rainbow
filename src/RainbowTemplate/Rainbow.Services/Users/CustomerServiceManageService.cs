using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Rainbow.Models;
using Rainbow.Services.Utils;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.Users
{
    public class CustomerServiceManageService : ServiceBase, ICustomerServiceManageService
    {
        public CustomerServiceManageService(ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus,
            IIdentityService identityService,
            SecurityUtil util, IOptions<JwtSettings> jwtOptions)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            IdentityService = identityService;
            Util = util;
            Settings = jwtOptions.Value;
        }

        private IIdentityService IdentityService { get; }
        private SecurityUtil Util { get; }
        private JwtSettings Settings { get; }

        public async Task InitBuildCustomerService()
        {
            using (var conn = GetConnection())
            {
                foreach (var index in Enumerable.Range(1, 10))
                {
                    var tmp = EntityFactory.Create<UserInfo>();
                    tmp.Phone = $"1890000{index:0000}";
                    tmp.PasswordHash = Util.Encoding($"TestUser@{index:0000}");
                    tmp.IsActive = true;
                    tmp.Name = $"TestUser:{index:0000}";

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
            }
        }

        public async Task<AsyncTaskTResult<LoginResultVM>> Login(LoginVM vm)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Phone == vm.Phone);
                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.PasswordHash))
                        return AsyncTaskResult.Failed<LoginResultVM>("账号不允许使用密码登陆");

                    if (!user.IsActive) return AsyncTaskResult.Failed<LoginResultVM>("账号未激活");

                    var pwdHash = Util.Encoding(vm.Password);
                    if (string.Equals(user.PasswordHash, pwdHash, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var (signId, endDate) = IdentityService.Login(user.Id);
                        var claims = new[]
                        {
                            new Claim("jti", user.Id.ToString(), ClaimValueTypes.String),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                            new Claim(ClaimTypes.MobilePhone, user.Phone),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim("signId", signId.ToString()),
                            new Claim(ClaimTypes.Role, string.Join(",", await GetCustomerServiceRoles(user.Id)))
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SecretKey));
                        //签名证书(秘钥，加密算法)
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间

                        var token = new JwtSecurityToken(Settings.Issuer, Settings.Audience, claims, DateTime.Now,
                            endDate, creds);


                        return AsyncTaskResult.Success(new LoginResultVM
                        {
                            IsSuccess = true,
                            Message = new JwtSecurityTokenHandler().WriteToken(token)
                        });
                    }
                }
                else
                {
                    return AsyncTaskResult.Failed<LoginResultVM>("账号不存在");
                }
            }

            return null;
        }

        public async Task<AsyncTaskTResult<CustomerServiceVM>> GetCustomerService(Guid id)
        {
            using (var conn = GetConnection())
            {
                var item = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Id == id);
                return new AsyncTaskTResult<CustomerServiceVM>(new CustomerServiceVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    Roles = await GetCustomerServiceRoles(id)
                });
            }
        }

        public async Task<bool> IsCustomerService(Guid id)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Id == id);
                var customerRole =
                    await conn.FirstOrDefaultAsync<RoleInfo>(a => a.Name == UserRoleType.CustomerService.ToString());

                if (await conn.ExistAsync<UserRole>(a => a.UserId == user.Id && a.RoleId == customerRole.Id))
                    return true;
            }

            return false;
        }

        public async Task<bool> IsCustomerService(string phone)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Phone == phone);
                var customerRole =
                    await conn.FirstOrDefaultAsync<RoleInfo>(a => a.Name == UserRoleType.CustomerService.ToString());

                if (await conn.ExistAsync<UserRole>(a => a.UserId == user.Id && a.RoleId == customerRole.Id))
                    return true;
            }

            return false;
        }

        public async Task<AsyncTaskTResult<bool>> Logout(Guid id)
        {
            using (var conn = GetConnection())
            {
                IdentityService.Logout(id);
            }

            return null;
        }

        public async Task<IEnumerable<string>> GetCustomerServiceRoles(Guid id)
        {
            using (var conn = GetConnection())
            {
                var roles = (await conn.AllAsync<RoleInfo>()).ToDictionary(a => a.Id);

                return (await conn.ListAsync<UserRole>(a => a.UserId == id)).Select(b => roles[b.RoleId].Name);
            }
        }
    }
}