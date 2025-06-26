using ShopEasy.API.Models;

namespace ShopEasy.API.Middleware
{
    public class GuestIdMiddleware
    {
        private readonly RequestDelegate _next;

        public GuestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                if (!context.Request.Cookies.ContainsKey("guestId"))
                {
                    var guestId = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("guestId", guestId, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    });

                    context.Items["guestId"] = guestId;
                }
                else
                {
                    context.Items["guestId"] = context.Request.Cookies["guestId"];
                   // context.User.Identity.IsAuthenticated = false;
                }
            }

            await _next(context);
        }
    }
}
