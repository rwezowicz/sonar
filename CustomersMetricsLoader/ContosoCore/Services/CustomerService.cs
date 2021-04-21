using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContosoCore.Interfaces;
using ContosoCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ContosoCore.Services
{
    public class CustomerService : BaseContosoService, ICustomerService
    {
        private readonly string _endPoint;

        public CustomerService(HttpClient client, IConfiguration config) :
            base(client, config)
        {
            _endPoint = config["SonarApi:CustomerEndpoint"];
        }

        /// <summary>
        /// Get a list of all customer
        /// </summary>
        /// <returns>List of Customers</returns>
        public async Task<List<Customer>> GetListAsync()
        {
            var list = await _client.GetStringAsync(_endPoint);
            return JsonConvert.DeserializeObject<List<Customer>>(list);
        }

        /// <summary>
        /// Retrieve an implementation of ICustomerService based on DI
        /// </summary>
        /// <param name="sp">Service Provider</param>
        /// <returns>CustomerService Object</returns>
        public static ICustomerService GetImplementation(IServiceProvider sp)
        {
            var config = sp.GetRequiredService<IConfigurationRoot>();
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient("ContosoHttpClient");
            return new CustomerService(httpClient, config);
        }
    }
}
