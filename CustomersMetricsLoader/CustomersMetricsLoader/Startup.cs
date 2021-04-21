using System;
using ContosoCore.Context;
using ContosoCore.Interfaces;
using ContosoCore.Managers;
using ContosoCore.Services;
using CustomersMetricsLoader.Runners;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<CustomersMetricsDatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

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

            services.AddScoped<ILoaderManager, LoaderManager>();

            services.AddScoped<LoaderRunner>();
        }
    }
}
