using System;
using System.Threading.Tasks;
using ContosoCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContosoCore.Managers
{
    public class LoaderManager : ILoaderManager
    {

        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;
        private readonly IMetricsService _metricsService;

        public LoaderManager(ILogger<LoaderManager> logger, ICustomerService customerService, IMetricsService metricsService)
        {
            _logger = logger;
            _customerService = customerService;
            _metricsService = metricsService;
        }

        public async Task Run()
        {
            _logger.LogInformation($"LoaderManager Started at {DateTime.UtcNow}");

            var customers = await _customerService.GetListAsync();

            Console.WriteLine("--- Customers ---");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.name);
            }

            var metrics = await _metricsService.GetListAsync(1);

            Console.WriteLine("--- Metrics ---");
            foreach (var metric in metrics)
            {
                Console.WriteLine(metric.name);
            }

            Console.ReadKey();

            _logger.LogInformation($"LoaderManager Completed at {DateTime.UtcNow}");
        }
    }
}
