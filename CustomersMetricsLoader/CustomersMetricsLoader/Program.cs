using System;
using System.Threading.Tasks;
using ContosoCore.Managers;
using ContosoCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersMetricsLoader
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            var startup = new Startup();
            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                var service = serviceProvider.GetService<LoaderManager>();
                await service.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occurred {ex}");
            }
        }
    }
}