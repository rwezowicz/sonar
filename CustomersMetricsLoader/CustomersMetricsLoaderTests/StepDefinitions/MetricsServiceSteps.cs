using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCore.Models;
using ContosoCore.Services;
using CustomersMetricsLoaderTests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class MetricsServiceSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public MetricsServiceSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have a list of metrics data")]
        public void GivenIHaveAListOfMetricsData(Table table)
        {
            var metric = table.CreateSet<Metric>().ToList();

            _scenarioContext["InitialMetric"] = metric;
        }

        [Given(@"I have a metrics service")]
        public void GivenIHaveAMetricsService()
        {
            var metric = _scenarioContext.Get<List<Metric>>("InitialMetric");
            var client = TestHelpers.GenerateHttpClientMock(metric);
            var config = _scenarioContext.Get<IConfigurationRoot>("TestConfiguration");
            _scenarioContext["MetricsService"] = new MetricsService(client, config);
        }

        [When(@"I request a list of metrics data for customer_id (.*)")]
        public async Task WhenIRequestAListOfMetricsDataForCustomer_Id(int id)
        {
            var service = _scenarioContext.Get<MetricsService>("MetricsService");
            var metric = await service.GetListForCustomerId(id);
            _scenarioContext["RetrievedMetricListForCustomer"] = metric;
        }

        [Then(@"the following metric is returned")]
        public void ThenTheFollowingMetricIsReturned(Table table)
        {
            var retrievedMetricList = _scenarioContext.Get<List<Metric>>("RetrievedMetricListForCustomer");

            var expectedMetricList = table.CreateSet<Metric>().ToList();

            Assert.That(retrievedMetricList[0].id == expectedMetricList[0].id);
            Assert.That(expectedMetricList.First().id == retrievedMetricList[0].id);
            Assert.That(expectedMetricList.First().customer_id == retrievedMetricList[0].customer_id);
            Assert.That(expectedMetricList.First().expression == retrievedMetricList[0].expression);
            Assert.That(expectedMetricList.First().name == retrievedMetricList[0].name);
        }

        [When(@"I request an implementation of ICustomerIMetricsServiceService")]
        public void WhenIRequestAnImplementationOfICustomerIMetricsServiceService()
        {
            var serviceProvider = _scenarioContext.Get<ServiceProvider>("ServiceProvider");
            var metricsService = MetricsService.GetImplementation(serviceProvider);

            _scenarioContext["MetricsService"] = metricsService;
        }

        [Then(@"I receive a MetricsService object")]
        public void ThenIReceiveAMetricsServiceObject()
        {
            var service = _scenarioContext.Get<MetricsService>("MetricsService");
            Assert.IsInstanceOf<MetricsService>(service);
        }
    }
}