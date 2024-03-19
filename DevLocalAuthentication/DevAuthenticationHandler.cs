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
            var principal = devLocalConfig.GetDevPrincipleOrNull();
            if (principal == null)
            {
                return AuthenticateResult.NoResult();
            }
            var authenticationTicket = new AuthenticationTicket(principal, DevAuthOption.Scheme);
            return AuthenticateResult.Success(authenticationTicket);
        }
    }
}
