namespace ShopEasy.API.Services.Notifications
{
    public class EmailNotification : INotification
    {
        public void send(string message)
        {
            Console.WriteLine($"[Email] : {message}");
        }
    }
}
