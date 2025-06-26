using ShopEasy.API.Services.Notifications;

namespace ShopEasy.API.Interfaces
{
    public interface INotificationFactory
    {
        INotification Create(string type);
    }
}
