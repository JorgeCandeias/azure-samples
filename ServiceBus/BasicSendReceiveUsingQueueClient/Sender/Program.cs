using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Sender
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                    config.AddUserSecrets<Program>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });

                    services.Configure<SenderOptions>(options =>
                    {
                        options.ConnectionString = context.Configuration.GetConnectionString("ServiceBus");
                        options.QueueName = context.Configuration["ServiceBus:Queue:Name"];
                    });

                    services.AddHostedService<SenderHostedService>();
                })
                .RunConsoleAsync();
        }
    }
}