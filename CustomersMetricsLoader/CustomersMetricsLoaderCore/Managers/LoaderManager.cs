using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCore.Context;
using ContosoCore.Interfaces;
using ContosoCore.Models;
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

        public async Task SaveAllCustomers(List<Customer> customers)
        {
            try
            {
                int added = 0;
                int updated = 0;

                using (var context = new CustomersMetricsDatabaseContext())
                {
                    foreach (var customer in customers)
                    {
                        if (!context.Customers.Any(x => x.id == customer.id))
                        {
                            await context.Customers.AddAsync(customer);
                            added++;
                        }
                        else
                        {
                            context.Customers.Update(customer);
                            updated++;
                        }
                    }

                    await context.SaveChangesAsync();
                }

                _logger.LogInformation($"Customers - Added: {added} | Updated {updated}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred when saving customers: {ex}");
            }
        }

        public async Task SaveAllMetrics(List<Metrics> metricsList)
        {
            try
            {
                int added = 0;
                int updated = 0;

                using (var context = new CustomersMetricsDatabaseContext())
                {
                    foreach (var metrics in metricsList)
                    {
                        if (!context.Metrics.Any(x => x.id == metrics.id))
                        {
                            await context.Metrics.AddAsync(metrics);
                            added++;
                        }
                        else
                        {
                            context.Metrics.Update(metrics);
                            updated++;
                        }
                    }

                    await context.SaveChangesAsync();
                }

                _logger.LogInformation($"Metrics - Added: {added} | Updated {updated}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred when saving metrics: {ex}");
            }
        }
    }
}