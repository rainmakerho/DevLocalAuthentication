using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DevLocalAuthentication
{
    public class DevAuthenticationHandler : AuthenticationHandler<DevAuthOption>
    {
        private readonly IConfiguration _configuration;


        public DevAuthenticationHandler(IOptionsMonitor<DevAuthOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock
            , IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var devLocalConfig = _configuration.GetSection(DevAuthOption.Scheme);
            if (!devLocalConfig.Exists())
            {
                return AuthenticateResult.NoResult();
            }
            var claimsData = devLocalConfig.GetChildren().ToDictionary(x => x.Key, x => x.Value);
            var claims = new List<Claim>();
            foreach (var claim in claimsData)
            {
                var claimType = GetClaimTypeByName(claim.Key);
                claims.Add(new Claim(claimType, claim.Value));
            }
            var claimsIdentity = new ClaimsIdentity(claims, DevAuthOption.Scheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, DevAuthOption.Scheme);
            return AuthenticateResult.Success(authenticationTicket);
        }

        private string GetClaimTypeByName(string name)
        {
            var claimType = typeof(ClaimTypes).GetField(name)?.GetValue(null);
            return (string)(claimType ?? name);
        }
    }
}
