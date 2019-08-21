using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using Rainbow.Services.Users;

namespace Rainbow.Authorize
{
    public class RainbowClaimsTransformation : IClaimsTransformation
    {
        public RainbowClaimsTransformation(IIdentityService service)
        {
            Service = service;
        }

        private IIdentityService Service { get; }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var transformed = new ClaimsPrincipal();
            if (principal.Identity?.IsAuthenticated == true)
            {
                var claim = principal.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Sub);
                var signClaim = principal.Claims.FirstOrDefault(a => a.Type == "signId");
                if (Guid.TryParse(claim?.Value, out var id) && Guid.TryParse(signClaim?.Value, out var signId))
                    if (Service.IsLogin(id, signId))
                        transformed.AddIdentities(principal.Identities);
            }

            return Task.FromResult(transformed);
        }
    }
}