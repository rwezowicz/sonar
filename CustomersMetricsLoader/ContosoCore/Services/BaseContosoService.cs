using System.Net.Http;
using ContosoCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ContosoCore.Services
{
    public abstract class BaseContosoService
    {
        protected readonly HttpClient _client;
        protected readonly IConfiguration _config;

        protected BaseContosoService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }
    }
}
