using System.Collections.Generic;
using System.Linq;
using ContosoCore.Context;
using ContosoCore.Interfaces;
using ContosoCore.Managers;
using ContosoCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class CommonTestSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonTestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have an api that will return the following list of metrics data")]
        public void GivenIHaveAnApiThatWillReturnTheFollowingListOfMetricsData(Table table)
        {
            var metric = table.CreateSet<Metrics>().ToList();
            _scenarioContext["ApiMetricsList"] = metric;
        }

        [Given(@"I have an api that will return the following list of customer data")]
        public void GivenIHaveAnApiThatWillReturnTheFollowingListOfCustomerData(Table table)
        {
            var customerList = table.CreateSet<Customer>();
            _scenarioContext["ApiCustomerList"] = customerList;
        }

        [Given(@"I have a service collection")]
        public void GivenIHaveAServiceCollection()
        {
            var config = _scenarioContext.Get<IConfigurationRoot>("TestConfiguration");

            var services = new ServiceCollection();
            services.AddSingleton(config);
            services.AddHttpClient();

            var serviceProvider = services.BuildServiceProvider();

            _scenarioContext["ServiceProvider"] = serviceProvider;
        }

        [Given(@"I have these customers in my database")]
        public void GivenIHaveTheseCustomersInMyDatabase(Table table)
        {
            var customerList = table.CreateSet<Customer>().AsQueryable();
            var customerDbSet = new Mock<DbSet<Customer>>();
            customerDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerList.Provider);
            customerDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerList.Expression);
            customerDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerList.ElementType);
            customerDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerList.GetEnumerator());

            _scenarioContext["CustomerDbSet"] = customerDbSet.Object;
        }

        [Given(@"I have these metrics in my database")]
        public void GivenIHaveTheseMetricsInMyDatabase(Table table)
        {
            var metricsList = table.CreateSet<Metrics>().AsQueryable();
            var metricsDbSet = new Mock<DbSet<Metrics>>();
            metricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.Provider).Returns(metricsList.Provider);
            metricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.Expression).Returns(metricsList.Expression);
            metricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.ElementType).Returns(metricsList.ElementType);
            metricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.GetEnumerator()).Returns(metricsList.GetEnumerator());

            _scenarioContext["MetricsDbSet"] = metricsDbSet.Object;
        }

        [Given(@"I have a loader manager")]
        public void GivenIHaveALoaderManager()
        {
            var mockLogger = new Mock<ILogger<LoaderManager>>();

            if (!_scenarioContext.TryGetValue("CustomerDbSet", out DbSet<Customer> customerDbSet))
            {
                var customerList = new List<Customer> { new Customer { id = 1 } }.AsQueryable();
                var mockCustomerDbSet = new Mock<DbSet<Customer>>();
                mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerList.Provider);
                mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerList.Expression);
                mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerList.ElementType);
                mockCustomerDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customerList.GetEnumerator());
                customerDbSet = mockCustomerDbSet.Object;
            }

            if (!_scenarioContext.TryGetValue("MetricsDbSet", out DbSet<Metrics> metricsDbSet))
            {
                var metricsList = new List<Metrics> { new Metrics { id = 1 } }.AsQueryable();
                var mockMetricsDbSet = new Mock<DbSet<Metrics>>();
                mockMetricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.Provider).Returns(metricsList.Provider);
                mockMetricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.Expression).Returns(metricsList.Expression);
                mockMetricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.ElementType).Returns(metricsList.ElementType);
                mockMetricsDbSet.As<IQueryable<Metrics>>().Setup(m => m.GetEnumerator()).Returns(metricsList.GetEnumerator());
                metricsDbSet = mockMetricsDbSet.Object;
            }

            if (!_scenarioContext.TryGetValue("ApiCustomerList", out List<Customer> apiCustomerList))
            {
                apiCustomerList = new List<Customer>
                {
                    new Customer {id = 1}
                };
            }

            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService
                .Setup(m => m.GetListAsync())
                .ReturnsAsync(apiCustomerList);

            if (!_scenarioContext.TryGetValue("ApiMetricsList", out List<Metrics> apiMetricsList))
            {
                apiMetricsList = new List<Metrics>
                {
                    new Metrics {id = 1}
                };
            }

            var mockMetricsService = new Mock<IMetricsService>();
            mockMetricsService
                .Setup(m => m.GetListForCustomerId(It.IsAny<int>()))
                .ReturnsAsync(apiMetricsList);

            var mockCustomersMetricsDatabaseContext = new Mock<CustomersMetricsDatabaseContext>();
            mockCustomersMetricsDatabaseContext.Setup(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>())).Verifiable();
            mockCustomersMetricsDatabaseContext.Setup(s => s.Customers).Returns(customerDbSet);
            mockCustomersMetricsDatabaseContext.Setup(s => s.Metrics).Returns(metricsDbSet);

            var manager = new LoaderManager(
                mockLogger.Object,
                mockCustomerService.Object,
                mockMetricsService.Object,
                mockCustomersMetricsDatabaseContext.Object
            );

            _scenarioContext["LoaderManager"] = manager;
            _scenarioContext["CustomerListCount"] = customerDbSet.Count();
            _scenarioContext["MetricsListCount"] = metricsDbSet.Count();

            _scenarioContext["MockCustomersMetricsDatabaseContext"] = mockCustomersMetricsDatabaseContext;
            _scenarioContext["MockLoggerLoaderManager"] = mockLogger;
        }
    }
}
