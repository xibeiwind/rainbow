using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Rainbow.Services.Users;

namespace Rainbow.Platform.WebAPP
{
    public class RainbowClaimsTransformation : IClaimsTransformation
    {
        private IIdentityService Service { get; }

        public RainbowClaimsTransformation(IIdentityService service)
        {
            Service = service;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var transformed = new ClaimsPrincipal();
            if (principal.Identity?.IsAuthenticated == true)
            {
                var claim = principal.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Sub);
                var signClaim = principal.Claims.FirstOrDefault(a => a.Type == "signId");
                if (Guid.TryParse(claim?.Value, out var id) && Guid.TryParse(signClaim?.Value, out var signId))
                {
                    if (Service.IsLogin(id, signId))
                    {
                        transformed.AddIdentities(principal.Identities);
                    }
                }
            }

            return transformed;
        }
    }
}