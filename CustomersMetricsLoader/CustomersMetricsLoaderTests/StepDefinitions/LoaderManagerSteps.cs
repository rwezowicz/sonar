using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCore.Context;
using ContosoCore.Managers;
using ContosoCore.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class LoaderManagerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public LoaderManagerSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the database isn't working correctly")]
        public void GivenTheDatabaseIsnTWorkingCorrectly()
        {
            var mockCustomersMetricsDatabaseContext = _scenarioContext.Get<Mock<CustomersMetricsDatabaseContext>>("MockCustomersMetricsDatabaseContext");

            mockCustomersMetricsDatabaseContext
                .Setup(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()))
                .Throws(new Exception("error"));
        }

        [Given(@"I want to update these customers")]
        public void GivenIWantToUpdateTheseCustomers(Table table)
        {
            var customerList = table.CreateSet<Customer>().AsQueryable();
            _scenarioContext["CustomersToUpdate"] = customerList.ToList();
        }

        [Given(@"I want to update these metrics")]
        public void GivenIWantToUpdateTheseMetrics(Table table)
        {
            var metricsList = table.CreateSet<Metrics>().AsQueryable();
            _scenarioContext["MetricsToUpdate"] = metricsList.ToList();
        }

        [When(@"I request a list of all customers")]
        public async Task WhenIRequestAListOfAllCustomers()
        {
            var manager = _scenarioContext.Get<LoaderManager>("LoaderManager");
            _scenarioContext["CustomerList"] = await manager.GetAllCustomers();
        }

        [When(@"I request a list of all metrics for customer id (.*)")]
        public async Task WhenIRequestAListOfAllMetricsForCustomerId(int id)
        {
            var manager = _scenarioContext.Get<LoaderManager>("LoaderManager");
            _scenarioContext["MetricsList"] = await manager.GetMetricsListForCustomerId(id);
        }

        [When(@"I save all the customer data")]
        public async Task WhenISaveAllTheCustomerData()
        {
            var manager = _scenarioContext.Get<LoaderManager>("LoaderManager");
            var customerList = _scenarioContext.Get<List<Customer>>("CustomersToUpdate");
            await manager.SaveAllCustomers(customerList);
        }

        [When(@"I save all the metrics data")]
        public async Task WhenISaveAllTheMetricsData()
        {
            var manager = _scenarioContext.Get<LoaderManager>("LoaderManager");
            var metricsList = _scenarioContext.Get<List<Metrics>>("MetricsToUpdate");

            try
            {
                await manager.SaveAllMetrics(metricsList);
            }
            catch (Exception ex)
            {
                _scenarioContext["Error"] = ex.Message;
            }
        }

        [Then(@"all customers will be returned")]
        public void ThenAllCustomersWillBeReturned()
        {
            var customerList = _scenarioContext.Get<List<Customer>>("CustomerList");
            var customerListCount = _scenarioContext.Get<int>("CustomerListCount");
            Assert.That(customerList.Count == customerListCount);
        }

        [Then(@"all metrics will be returned for that customer")]
        public void ThenAllMetricsWillBeReturnedForThatCustomer()
        {
            var metricsList = _scenarioContext.Get<List<Metrics>>("MetricsList");
            var metricsListCount = _scenarioContext.Get<int>("MetricsListCount");
            Assert.That(metricsList.Count == metricsListCount);
        }

        [Then(@"the data will successfully save")]
        public void ThenTheDataWillSuccessfullySave()
        {
            var dataContext = _scenarioContext.Get<Mock<CustomersMetricsDatabaseContext>>("MockCustomersMetricsDatabaseContext");
            dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()));
        }

        [Then(@"the log will show ""(.*)""")]
        public void ThenTheLogWillShow(string error)
        {
            var mockLogger = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLogger.Invocations[0].ToString().Contains(error));
        }
    }
}
