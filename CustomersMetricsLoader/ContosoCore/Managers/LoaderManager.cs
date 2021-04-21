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
        private CustomersMetricsDatabaseContext _context;

        public LoaderManager(ILogger<LoaderManager> logger, ICustomerService customerService, IMetricsService metricsService, CustomersMetricsDatabaseContext context)
        {
            _logger = logger;
            _customerService = customerService;
            _metricsService = metricsService;
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerService.GetListAsync();
        }

        public async Task<List<Metrics>> GetMetricsListForCustomerId(int id)
        {
            return await _metricsService.GetListForCustomerId(id);
        }

        public async Task SaveAllCustomers(List<Customer> customers)
        {
            try
            {
                var added = 0;
                var updated = 0;

                foreach (var customer in customers)
                {
                    if (!_context.Customers.Any(x => x.id == customer.id))
                    {
                        await _context.Customers.AddAsync(customer);
                        added++;
                    }
                    else
                    {
                        _context.Customers.Update(customer);
                        updated++;
                    }
                }

                await _context.SaveChangesAsync();

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
                var added = 0;
                var updated = 0;

                foreach (var metrics in metricsList)
                {
                    if (!_context.Metrics.Any(x => x.id == metrics.id))
                    {
                        await _context.Metrics.AddAsync(metrics);
                        added++;
                    }
                    else
                    {
                        _context.Metrics.Update(metrics);
                        updated++;
                    }
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Metrics - Added: {added} | Updated {updated}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred when saving metrics: {ex}");
            }
        }
    }
}