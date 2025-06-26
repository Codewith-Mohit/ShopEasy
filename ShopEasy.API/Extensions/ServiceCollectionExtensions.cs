using ShopEasy.API.Factory;
using ShopEasy.API.Helpers;
using ShopEasy.API.Interfaces;
using ShopEasy.API.Services;
using ShopEasy.API.Services.Notifications;

namespace ShopEasy.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.AddScoped<ICustomLoggerProvider, CustomLoggerProvider>();
            
            services.AddScoped<JwtTokenGenerator>();            

            services.AddTransient<INotificationFactory, NotificationFactory>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<NotificationService>();

            services.AddTransient<EmailNotification>();
            services.AddTransient<SmsNotification>();

            return services;
        }
    }
}
