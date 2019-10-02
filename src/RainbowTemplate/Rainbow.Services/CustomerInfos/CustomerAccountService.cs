using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Rainbow.Common;
using Rainbow.Common.Configs;
using Rainbow.Common.Enums;
using Rainbow.Common.Utils;
using Rainbow.Models;
using Rainbow.ViewModels.CustomerInfos;
using Senparc.Weixin;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Yunyong.Core;
using Yunyong.DataExchange;
using Yunyong.EventBus;

namespace Rainbow.Services.CustomerInfos
{
    public class CustomerAccountService : ServiceBase, ICustomerAccountService
    {
        private JwtSettings JwtSettings { get; }
        private WechatSettings WechatSettings { get; }
        private ICustomerIdentityService Service { get; }

        /// <summary>
        /// </summary>
        public CustomerAccountService(
            ConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            IEventBus eventBus,
            JwtSettings jwtSettings,
            WechatSettings wechatSettings,
            ICustomerIdentityService service
            ) : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
            JwtSettings = jwtSettings;
            WechatSettings = wechatSettings;
            Service = service;
        }

        #region Implementation of ICustomerAccountService

        /// <summary>
        ///     检查用户是否登陆
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="signId"></param>
        /// <returns></returns>
        public async Task<bool> IsLogin(Guid customerId, Guid signId)
        {
            using (var conn = GetConnection())
            {
                if (!await conn.ExistAsync<CustomerInfo>(a => a.Id == customerId))
                    return false;
            }

            return Service.IsLogin(customerId, signId);
        }

        /// <summary>
        ///     用户登陆
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<WechatLoginResultVM> Login(WechatLoginVM vm)
        {
            using (var conn = GetConnection())
            {
                var tmp = new JsCode2Session
                {
                    OpenId = "DemoOPenId",
                    SessionKey = "demo_session_key",
                    UnionId = "demo_union_id"
                };

                if (vm.LoginCode == "this_is_a_mock_login_code")
                {
                    // skip get openid,
                }
                else
                {
                    // code换OpenId

                    var jsonResult = SnsApi.JsCode2Json(WechatSettings.AppId, WechatSettings.AppSecret, vm.LoginCode);
                    if (jsonResult.errcode == ReturnCode.请求成功)
                    {
                        string uniondId = "";
                        SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, uniondId);

                        tmp.OpenId = jsonResult.openid;
                        tmp.SessionKey = jsonResult.session_key;
                        tmp.UnionId = jsonResult.unionid;
                    }
                }

                var info = await conn.FirstOrDefaultAsync<CustomerInfo>(a => a.OpenId == tmp.OpenId);
                if (info == null)
                {
                    info = EntityFactory.Create<CustomerInfo>();
                    info.OpenId = tmp.OpenId;
                    info.UnionId = tmp.UnionId;
                    info.NickName = vm.NickName;
                    info.AvatarUrl = vm.AvatarUrl;
                    await conn.CreateAsync(info);
                }

                var trackVM = Service.LoginTrack(info.Id);

                var claims = new List<Claim>
                             {
                                 new Claim("jti", info.Id.ToString(), ClaimValueTypes.String),
                                 new Claim(JwtRegisteredClaimNames.Sub, info.Id.ToString()),
                                 new Claim(ClaimTypes.Name, info.NickName),
                                 new Claim("signId", trackVM.SignId.ToString()),
                                 new Claim("CustomerId", $"{info.Id:D}"),
                                 new Claim(ClaimTypes.Role, nameof(UserRoleType.Customer))
                             };

                //claims.AddRange((await GetCustomerRoles(info.Id)).Select(a => new Claim(ClaimTypes.Role, a)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
                //签名证书(秘钥，加密算法)
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，
                //并引用System.IdentityModel.Tokens.Jwt命名空间

                var token = new JwtSecurityToken(JwtSettings.Issuer, JwtSettings.Audience, claims, DateTime.Now,
                                                 trackVM.ExpiresTime, signingCredentials);

                return new WechatLoginResultVM
                {
                    IsSuccess = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
        }

        /// <summary>
        ///     用户登出
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public AsyncTaskTResult<bool> Logout(Guid customerId)
        {
            Service.Logout(customerId);
            return AsyncTaskResult.Success(true);
        }

        #endregion
    }
}
