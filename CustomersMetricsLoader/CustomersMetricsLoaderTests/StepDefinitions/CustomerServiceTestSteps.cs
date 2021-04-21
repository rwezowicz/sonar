using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoCore.Models;
using ContosoCore.Services;
using CustomersMetricsLoaderTests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CustomersMetricsLoaderTests.StepDefinitions
{
    [Binding]
    public class CustomerServiceTestSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CustomerServiceTestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have a customer service")]
        public void GivenIHaveACustomerService()
        {
            var customerList = _scenarioContext.Get<List<Customer>>("ApiCustomerList");
            var client = TestHelpers.GenerateHttpClientMock(customerList);
            var config = _scenarioContext.Get<IConfigurationRoot>("TestConfiguration");
            _scenarioContext["CustomerService"] = new CustomerService(client, config);
        }

        [When(@"I request a list of all customer data")]
        public async Task WhenIRequestAListOfAllCustomerData()
        {
            var service = _scenarioContext.Get<CustomerService>("CustomerService");
            var customerList = await service.GetListAsync();
            _scenarioContext["RetrievedCustomerList"] = customerList;
        }

        [Then(@"the api returns an entire customer data list")]
        public void ThenTheApiReturnsAnEntireCustomerDataList()
        {
            var ApiCustomerList = _scenarioContext.Get<List<Customer>>("ApiCustomerList");
            var retrievedCustomerList = _scenarioContext.Get<List<Customer>>("RetrievedCustomerList");
            Assert.That(ApiCustomerList.Count == retrievedCustomerList.Count);
        }

        [When(@"I request an implementation of ICustomerService")]
        public void WhenIRequestAnImplementationOfICustomerService()
        {
            var serviceProvider = _scenarioContext.Get<ServiceProvider>("ServiceProvider");
            var customerService = CustomerService.GetImplementation(serviceProvider);

            _scenarioContext["CustomerService"] = customerService;
        }

        [Then(@"I receive a CustomerService object")]
        public void ThenIReceiveACustomerServiceObject()
        {
            var service = _scenarioContext.Get<CustomerService>("CustomerService");
            Assert.IsInstanceOf<CustomerService>(service);
        }


    }
}
