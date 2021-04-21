using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Models;

namespace ContosoCore.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetListAsync();
    }
}
