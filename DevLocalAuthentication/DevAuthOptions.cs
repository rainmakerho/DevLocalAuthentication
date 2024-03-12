using Microsoft.AspNetCore.Authentication;

namespace DevLocalAuthentication
{
    public class DevAuthOption : AuthenticationSchemeOptions
    {
        public const string Scheme = DevAuthenticationDefaults.AuthenticationScheme;
    }
}
