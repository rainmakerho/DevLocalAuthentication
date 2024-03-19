using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevLocalAuthentication
{
    public class DevLocalAuthMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public DevLocalAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }
            var devLocalConfig = _configuration.GetSection(DevAuthOption.Scheme);
            var principal = devLocalConfig.GetDevPrincipleOrNull();
            if (principal == null)
            {
                await _next(context);
                return;
            }
            context.User = principal;
            await _next(context);
        }
    }
}
