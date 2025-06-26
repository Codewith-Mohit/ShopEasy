using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;

namespace ShopEasy.API.Middleware
{
    public class AccessTokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AccessTokenValidatorMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var tokenExists = context.Request.Headers.TryGetValue("AccessKey", out var token);

            //if (tokenExists && _config["AccessKey"] == token)
                await _next.Invoke(context);
            //else
            //{
            //    context.Response.StatusCode = 400;
            //    await context.Response.WriteAsync("Invalid Token... ");
            //}
        }
    }
}
