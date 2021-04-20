using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CustomersMetricsLoaderTests.Helpers
{
    public static class TestHelpers
    {
        public static HttpClient GenerateHttpClientMock(object apiJson, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var jsonString = JsonConvert.SerializeObject(apiJson);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage() { StatusCode = httpStatusCode, Content = new StringContent(jsonString) })
                .Verifiable();

            var client = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            return client;
        }
    }
}
