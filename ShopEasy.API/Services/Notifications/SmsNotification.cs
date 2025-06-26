namespace ShopEasy.API.Services.Notifications
{
    public class SmsNotification : INotification
    {
        public void send(string message)
        {
            Console.WriteLine($"[SMS] : {message}");
        }
    }
}
