using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

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
    }
}
