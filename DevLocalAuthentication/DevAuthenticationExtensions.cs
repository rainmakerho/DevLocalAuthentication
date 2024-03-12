using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
