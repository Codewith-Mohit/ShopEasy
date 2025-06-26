using ShopEasy.API.Interfaces;
using ShopEasy.API.Services.Notifications;

namespace ShopEasy.API.Factory
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;  

        public NotificationFactory(IServiceProvider serviceProvider)
        {
                _serviceProvider = serviceProvider; 
        }

        public INotification Create(string type)
        {
            return type.ToLower() switch
            {
                "email" => _serviceProvider.GetRequiredService<EmailNotification>(),
                "sms" => _serviceProvider.GetRequiredService<SmsNotification>(),
                //"push" => _serviceProvider.GetRequiredService<EmailNotification>(),
                _ => throw new ArgumentException($"Unsupported notification type : {type} ")
            };
        }
    }
}
