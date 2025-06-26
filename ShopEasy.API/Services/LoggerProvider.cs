using Microsoft.Extensions.Logging;

namespace ShopEasy.API.Services
{
    public class CustomLoggerProvider : ICustomLoggerProvider
    {
        public void writemsg(string str)
        {
            Console.WriteLine($"Message from my service provider :{str}");
        }       
    }


    public interface ICustomLoggerProvider
    {
        void writemsg(string msg);
    }
}
