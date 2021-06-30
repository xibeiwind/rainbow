using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Rainbow.MP.Authorize
{
    public class RainbowSecurityTokenValidator : ISecurityTokenValidator
    {
        bool ISecurityTokenValidator.CanValidateToken => true;

        int ISecurityTokenValidator.MaximumTokenSizeInBytes { get; set; }

        bool ISecurityTokenValidator.CanReadToken(string securityToken)
        {
            return true;
        }

        //验证token
        ClaimsPrincipal ISecurityTokenValidator.ValidateToken(string securityToken,
            TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;

            var token = new JwtSecurityToken(securityToken);

            //给Identity赋值
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(token.Claims);
            var principle = new ClaimsPrincipal(identity);
            validatedToken = token;

            return principle;
        }
    }
}