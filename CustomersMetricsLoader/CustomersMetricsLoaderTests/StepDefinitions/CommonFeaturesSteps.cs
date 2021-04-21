using System.Linq;
using ContosoCore.Context;
using ContosoCore.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class CommonFeaturesSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonFeaturesSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I create a CustomersMetricsDatabaseContext with options")]
        public void WhenICreateACustomersMetricsDatabaseContextWithOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomersMetricsDatabaseContext>();
            optionsBuilder.UseSqlServer("test_connection_string");

            var dbContext = new CustomersMetricsDatabaseContext(optionsBuilder.Options);
            _scenarioContext["CustomersMetricsDatabaseContext"] = dbContext;
        }

        [When(@"I create a CustomersMetricsDatabaseContext without options")]
        public void WhenICreateACustomersMetricsDatabaseContextWithoutOptions()
        {
            var dbContext = new CustomersMetricsDatabaseContext();
            _scenarioContext["CustomersMetricsDatabaseContext"] = dbContext;
        }

        [When(@"I set the customer and metrics db set appropriately")]
        public void WhenISetTheCustomerAndMetricsDbSetAppropriately()
        {
            var customerDbSet = _scenarioContext.Get<DbSet<Customer>>("CustomerDbSet");
            var metricsDbSet = _scenarioContext.Get<DbSet<Metrics>>("MetricsDbSet");

            var dbContext = _scenarioContext.Get<CustomersMetricsDatabaseContext>("CustomersMetricsDatabaseContext");
            dbContext.Customers = customerDbSet;
            dbContext.Metrics = metricsDbSet;
        }

        [Then(@"It is created without incident")]
        public void ThenItIsCreatedWithoutIncident()
        {
            var dbContext = _scenarioContext.Get<CustomersMetricsDatabaseContext>("CustomersMetricsDatabaseContext");
            Assert.IsInstanceOf<CustomersMetricsDatabaseContext>(dbContext);
        }

        [Then(@"customers and metrics are retrieved appropriately")]
        public void ThenCustomersAndMetricsAreRetrievedAppropriately()
        {
            var dbContext = _scenarioContext.Get<CustomersMetricsDatabaseContext>("CustomersMetricsDatabaseContext");
            Assert.NotZero(dbContext.Customers.Count());
            Assert.NotZero(dbContext.Metrics.Count());
        }

    }
}
