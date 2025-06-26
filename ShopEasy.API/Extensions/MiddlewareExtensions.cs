using Microsoft.AspNetCore.Builder;
using ShopEasy.API.Middleware;

namespace ShopEasy.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseGuestMiddleWare(this IApplicationBuilder app) => app.UseMiddleware<GuestIdMiddleware>();

        public static IApplicationBuilder UseAccessTokenValidatorMiddleWare(this IApplicationBuilder app) => app.UseMiddleware<AccessTokenValidatorMiddleware>();

    }

    
}
