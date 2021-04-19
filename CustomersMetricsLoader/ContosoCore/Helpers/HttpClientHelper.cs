using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoCore.Helpers
{
    public static class HttpClientHelper
    {
        private static string ContosoHttpClientName = "ContosoHttpClient";

        public static HttpClient CreateHttpClient(IServiceProvider cs)
        {
            var clientFactory = cs.GetRequiredService<IHttpClientFactory>();
            return clientFactory.CreateClient(ContosoHttpClientName);
        }
    }
}
