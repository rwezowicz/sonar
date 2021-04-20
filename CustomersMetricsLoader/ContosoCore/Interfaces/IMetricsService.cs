using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Models;

namespace ContosoCore.Interfaces
{
    public interface IMetricsService : IContosoService
    {
        Task<List<Metric>> GetListForCustomerId(int id);
    }
}
