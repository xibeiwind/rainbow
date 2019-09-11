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
using Rainbow.Events;
using Rainbow.Models;
using Rainbow.Services.Utils;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Rainbow.Services.Users
{
    public class UserAccountService : ServiceBase, IUserAccountService
    {
        public UserAccountService(
            ConnectionSettings connectionSettings,
            SecurityUtil securityUtil,
            IConnectionFactory connectionFactory,
            IIdentityService identityService,
            IOptions<JwtSettings> jwtOptions,
            ILoggerFactory loggerFactory,
            IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            SecurityUtil = securityUtil;
            IdentityService = identityService;
            Settings = jwtOptions.Value;
        }

        private SecurityUtil SecurityUtil { get; }
        private IIdentityService IdentityService { get; }
        private JwtSettings Settings { get; }


        public async Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<UserInfo>(a => a.Phone == vm.Phone))
                    return AsyncTaskResult.Failed<UserVM>("电话已被使用");

                var user = EntityFactory.Create<UserInfo, RegisterUserVM>(vm);
                await conn.CreateAsync(user);

                return AsyncTaskResult.Success(await conn.FirstOrDefaultAsync<UserInfo, UserVM>(a => a.Id == user.Id));
            }
        }

        public async Task<LoginResultVM> SmsLoginAsync(SmsLoginVM vm)
        {
            var response =
                await EventBus.RequestAsync<SmsCodeVerifyRequest, SmsCodeVerifyResponse>(new SmsCodeVerifyRequest());
            if (response.IsSuccess)
                return new LoginResultVM {UserId = response.UserId, IsSuccess = true};
            return new LoginResultVM {IsSuccess = false, Message = response.ResultDesc};
        }

        public async Task<AsyncTaskResult> SendLoginSmsAsync(SendLoginSmsVM vm)
        {
            using (var conn = GetConnection())
            {
                if (await conn.ExistAsync<UserInfo>(a => a.Phone == vm.Phone && a.IsActive))
                {
                    var response = await EventBus.RequestAsync<SendSmsCodeRequest, SendSmsCodeResponse>(
                        new SendSmsCodeRequest {Phone = vm.Phone, Type = TplType.LoginSMS});
                    if (response.IsSuccess)
                        return AsyncTaskResult.Success();
                    return AsyncTaskResult.Failed(response.ResultDesc);
                }

                return AsyncTaskResult.Failed("账号不存在或未激活");
            }
        }

        public async Task<UserProfileVM> GetUserAsync(Guid userId)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Id == userId);

                return new UserProfileVM
                       {
                           Id = user.Id,
                           Name = user.Name,
                           AvatarUrl = user.AvatarUrl,
                           NickName = user.Name,
                           Phone = user.Phone,
                           Roles = await GetUserRoles(userId),
                           UserId = user.Id
                       };
            }
        }

        public async Task<LoginResultVM> PasswordLoginWithToken(LoginVM vm)
        {
            using (var conn = GetConnection())
            {
                var user = await conn.FirstOrDefaultAsync<UserInfo>(a => a.Phone == vm.Phone);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        if (string.IsNullOrEmpty(user.PasswordHash))
                            return new LoginResultVM {IsSuccess = false, Message = "账号不允许使用密码登陆"};

                        var pwdHash = SecurityUtil.Encoding(vm.Password);
                        if (string.Equals(user.PasswordHash, pwdHash, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var tmp = IdentityService.Login(user.Id);
                            var claims = new List<Claim>
                                         {
                                             new Claim("jti", user.Id.ToString(), ClaimValueTypes.String),
                                             new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                                             new Claim(ClaimTypes.MobilePhone, user.Phone),
                                             new Claim(ClaimTypes.Name, user.Name),
                                             new Claim("signId", tmp.SignId.ToString())
                                         };

                            claims.AddRange(
                                (await GetUserRoles(user.Id)).Select(role => new Claim(ClaimTypes.Role, role)));

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SecretKey));
                            //签名证书(秘钥，加密算法)
                            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间

                            var token = new JwtSecurityToken(Settings.Issuer, Settings.Audience, claims, DateTime.Now,
                                                             tmp.ExpiresTime, signingCredentials);

                            return new LoginResultVM
                                   {
                                       IsSuccess = true,
                                       UserId = user.Id,
                                       Message = new JwtSecurityTokenHandler().WriteToken(token)
                                   };
                        }

                        return new LoginResultVM {IsSuccess = false, Message = "账号密码不匹配"};
                    }

                    return new LoginResultVM {IsSuccess = false, Message = "账号未激活"};
                }

                return new LoginResultVM {IsSuccess = false, Message = "账号不存在"};
            }
        }

        public async Task<AsyncTaskTResult<bool>> UserInRole(Guid userId, string roleName)
        {
            using (var conn = GetConnection())
            {
                var role = await conn.FirstOrDefaultAsync<RoleInfo>(a => a.Name == roleName);
                if (role == null)
                    return AsyncTaskResult.Failed<bool>("角色不存在");

                var result = await conn.ExistAsync<UserRole>(a => a.UserId == userId && a.RoleId == role.Id);

                return AsyncTaskResult.Success(result);
            }
        }

        public Task<AsyncTaskTResult<bool>> Logout(Guid id)
        {
            IdentityService.Logout(id);

            return Task.FromResult(AsyncTaskResult.Success(true));
        }

        public async Task<bool> IsLogin(Guid userId, Guid signId)
        {
            using (var conn = GetConnection())
            {
                if (!await conn.ExistAsync<UserInfo>(a => a.Id == userId))
                    return false;
            }

            return IdentityService.IsLogin(userId, signId);
        }

        private async Task<IEnumerable<string>> GetUserRoles(Guid userId)
        {
            using (var conn = GetConnection())
            {
                var roles = (await conn.AllAsync<RoleInfo>()).ToDictionary(a => a.Id);

                return (await conn.ListAsync<UserRole>(a => a.UserId == userId))
                      .Where(b => roles.ContainsKey(b.RoleId))
                      .Select(b => roles[b.RoleId].Name);
            }
        }
    }
}