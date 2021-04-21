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
            var mockLoggerRunner = _scenarioContext.Get<Mock<ILogger<LoaderRunner>>>("MockLoggerLoaderRunner");
            var mockLoggerManager = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLoggerRunner.Invocations[0].ToString().Contains("LoaderManager Started"));
            Assert.That(mockLoggerRunner.Invocations[1].ToString().Contains("LoaderManager Completed"));
        }

        [Then(@"the number of added customers will be (.*)")]
        public void ThenTheNumberOfAddedCustomersWillBe(int p0)
        {
            var mockLoggerManager = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLoggerManager.Invocations[0].ToString().Contains($"Added: {p0}"));
        }

        [Then(@"the number of updated customers will be (.*)")]
        public void ThenTheNumberOfUpdatedCustomersWillBe(int p0)
        {
            var mockLoggerManager = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLoggerManager.Invocations[0].ToString().Contains($"Updated: {p0}"));
        }

        [Then(@"the number of added metrics will be (.*)")]
        public void ThenTheNumberOfAddedMetricsWillBe(int p0)
        {
            var mockLoggerManager = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLoggerManager.Invocations[1].ToString().Contains($"Added: {p0}"));
        }

        [Then(@"the number of updated metrics will be (.*)")]
        public void ThenTheNumberOfUpdatedMetricsWillBe(int p0)
        {
            var mockLoggerManager = _scenarioContext.Get<Mock<ILogger<LoaderManager>>>("MockLoggerLoaderManager");
            Assert.That(mockLoggerManager.Invocations[0].ToString().Contains($"Updated: {p0}"));
        }

    }
}
