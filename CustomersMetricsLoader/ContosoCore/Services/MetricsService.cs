using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContosoCore.Helpers;
using ContosoCore.Interfaces;
using ContosoCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ContosoCore.Services
{
    public class MetricsService : BaseContosoService, IMetricsService
    {
        private readonly string _endPoint;

        public MetricsService(HttpClient client, IConfiguration config) :
            base(client, config)
        {
            _endPoint = config["SonarApi:MetricsEndpoint"];
        }

        /// <summary>
        /// Get a list of all metrics for the passed customer id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>List of Metrics</returns>
        public async Task<List<Metrics>> GetListForCustomerId(int id)
        {
            var list = await _client.GetStringAsync($"{_endPoint}?customer_id={id}");

            return JsonConvert.DeserializeObject<List<Metrics>>(list);
        }

        /// <summary>
        /// Retrieve an implementation of IMetricsService based on DI
        /// </summary>
        /// <param name="sp">Service Provider</param>
        /// <returns>MetricsService Object</returns>
        public static IMetricsService GetImplementation(IServiceProvider sp)
        {
            var config = sp.GetRequiredService<IConfigurationRoot>();
            var httpClient = HttpClientHelper.CreateHttpClient(sp);
            return new MetricsService(httpClient, config);
        }
    }
}
