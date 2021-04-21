using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Interfaces;
using ContosoCore.Models;
using Microsoft.Extensions.Logging;

namespace CustomersMetricsLoader.Runners
{
    public class LoaderRunner
    {
        private readonly ILogger _logger;
        private readonly ILoaderManager _manager;

        public LoaderRunner(ILogger<LoaderRunner> logger, ILoaderManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        /// <summary>
        /// Run the Local Database Load of Customers and Metrics from API Requests
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            _logger.LogInformation($"LoaderManager Started at {DateTime.UtcNow}");

            var customers = await _manager.GetAllCustomers();
            await _manager.SaveAllCustomers(customers);

            var metricsList = new List<Metrics>();

            foreach (var customer in customers)
            {
                var customerIdMetrics = await _manager.GetMetricsListForCustomerId(customer.id);
                metricsList.AddRange(customerIdMetrics);
            }
            await _manager.SaveAllMetrics(metricsList);

            _logger.LogInformation($"LoaderManager Completed at {DateTime.UtcNow}");
        }
    }
}