using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace CustomersMetricsLoaderTests.Hooks
{
    [Binding]
    public class CustomerServiceHooks
    {
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenarioContext["TestConfiguration"] = GetTestConfiguration();
        }

        private IConfigurationRoot GetTestConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"SonarApi:CustomerEndpoint", "CustomerEndpoint"},
                {"SonarApi:MetricsEndpoint", "MetricsEndpoint"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }
    }
}