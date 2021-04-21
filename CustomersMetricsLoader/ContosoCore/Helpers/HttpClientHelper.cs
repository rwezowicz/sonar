using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoCore.Helpers
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// Create a ContosoHttpClient from the HttpClient Factory in DI
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static HttpClient CreateHttpClient(IServiceProvider cs)
        {
            var clientFactory = cs.GetRequiredService<IHttpClientFactory>();
            return clientFactory.CreateClient("ContosoHttpClient");
        }
    }
}
