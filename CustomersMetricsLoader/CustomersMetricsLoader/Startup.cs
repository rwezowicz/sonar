using System;
using ContosoCore.Managers;
using ContosoCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomersMetricsLoader
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        private const string HttpClientName = "ContosoHttpClient";

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddConfiguration(Configuration.GetSection("Logging"));
                config.AddConsole();
            });

            services.AddSingleton(Configuration);

            services.AddHttpClient(HttpClientName,
                ctx =>
                {
                    var baseUrl = Configuration["SonarApi:BaseUrl"];
                    ctx.BaseAddress = new Uri(baseUrl);
                });

            services.AddScoped(CustomerService.GetImplementation);

            services.AddScoped(MetricsService.GetImplementation);

            services.AddTransient<LoaderManager>();
        }
    }
}
