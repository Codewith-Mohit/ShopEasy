using ShopEasy.API.Interfaces;

namespace ShopEasy.API.Services.Notifications
{
    public class NotificationService
    {
        private readonly INotificationFactory _notificationfactory;

        public NotificationService(INotificationFactory  notificationFactory)
        {
            _notificationfactory = notificationFactory;                 
        }

        public void Notify(string Message, string type = "Email")        
        { 
            var notifier = _notificationfactory.Create(type);
            notifier.send(Message);

        }
    }
}
