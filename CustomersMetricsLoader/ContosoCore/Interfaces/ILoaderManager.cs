using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Models;

namespace ContosoCore.Interfaces
{
    public interface ILoaderManager
    {
        Task<List<Customer>> GetAllCustomers();

        Task<List<Metrics>> GetMetricsListForCustomerId(int id);

        Task SaveAllCustomers(List<Customer> customers);

        Task SaveAllMetrics(List<Metrics> metricsList);
    }
}