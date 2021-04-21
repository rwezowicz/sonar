using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Models;

namespace ContosoCore.Interfaces
{
    public interface IMetricsService
    {
        Task<List<Metrics>> GetListForCustomerId(int customer_id);
    }
}
