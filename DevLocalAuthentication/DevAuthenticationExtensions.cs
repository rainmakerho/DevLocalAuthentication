using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevLocalAuthentication
{
    public static class DevAuthenticationExtensions
    {
        public static AuthenticationBuilder AddDevLocalAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(DevAuthOption.Scheme)
                        .AddScheme<DevAuthOption, DevAuthenticationHandler>(DevAuthOption.Scheme, null);
        }

        public static void AddDevLocalAuthentication(this IServiceCollection services
            , IConfiguration configuration)
        {
            var devLocalConfig = configuration.GetSection(DevAuthOption.Scheme);
            if (devLocalConfig.Exists())
            {
                services.AddAuthentication(DevAuthOption.Scheme)
                        .AddScheme<DevAuthOption, DevAuthenticationHandler>(DevAuthOption.Scheme, null);
            }
        }


        public static ClaimsPrincipal GetDevPrincipleOrNull(this IConfigurationSection devLocalConfig)
        {
            if (!devLocalConfig.Exists()) return null;
            var isEnable = Convert.ToBoolean(devLocalConfig.GetSection("Enable")?.Value ?? "true");
            var claimsData = devLocalConfig.GetSection("Claims")?.GetChildren().ToDictionary(x => x.Key, x => x.Value);
            if(claimsData.Count==0) return null;

            var claims = new List<Claim>();
            foreach (var claim in claimsData)
            {
                var claimType = GetClaimTypeByName(claim.Key);
                claims.Add(new Claim(claimType, claim.Value));
            }
            var authenticationType = devLocalConfig.GetSection("AuthenticationType")?.Value ?? DevAuthOption.Scheme;
            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }

        private static string GetClaimTypeByName(string name)
        {
            var claimType = typeof(ClaimTypes).GetField(name)?.GetValue(null);
            return (string)(claimType ?? name);
        }


        public static void UseDevLocalAuthentication(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<DevLocalAuthMiddleware>();
        }
    }
}
