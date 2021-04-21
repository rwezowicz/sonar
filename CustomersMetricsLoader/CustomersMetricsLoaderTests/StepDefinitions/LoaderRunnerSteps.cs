using ContosoCore.Managers;
using CustomersMetricsLoader.Runners;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class LoaderRunnerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public LoaderRunnerSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have a loader runner")]
        public void GivenIHaveALoaderRunner()
        {
            var mockLogger = new Mock<ILogger<LoaderRunner>>();

            var manager = _scenarioContext.Get<LoaderManager>("LoaderManager");

            var runner = new LoaderRunner(
                mockLogger.Object,
                manager
            );

            _scenarioContext["LoaderRunner"] = runner;
            _scenarioContext["MockLoggerLoaderRunner"] = mockLogger;
        }

        [When(@"I run the load runner")]
        public void WhenIRunTheLoadRunner()
        {
            var runner = _scenarioContext.Get<LoaderRunner>("LoaderRunner");
            runner.Run();
        }

        [Then(@"all customers and metrics will be saved in the database")]
        public void ThenAllCustomersAndMetricsWillBeSavedInTheDatabase()
        {
            var mockLogger = _scenarioContext.Get<Mock<ILogger<LoaderRunner>>>("MockLoggerLoaderRunner");
            Assert.That(mockLogger.Invocations[0].ToString().Contains("LoaderManager Started"));
            Assert.That(mockLogger.Invocations[1].ToString().Contains("LoaderManager Completed"));
        }
    }
}
