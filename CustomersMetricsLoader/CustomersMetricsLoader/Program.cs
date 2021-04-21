using System;
using System.Threading.Tasks;
using CustomersMetricsLoader.Runners;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersMetricsLoader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            var startup = new Startup();
            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                var runner = serviceProvider.GetService<LoaderRunner>();
                await runner.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occurred {ex}");
            }
        }
    }
}