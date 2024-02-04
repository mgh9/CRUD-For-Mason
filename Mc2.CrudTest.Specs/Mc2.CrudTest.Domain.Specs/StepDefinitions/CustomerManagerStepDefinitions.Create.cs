using System.Net;
using System.Net.Http.Json;
using Mc2.CrudTest.Api.Infrastructure.ActionResults;
using Mc2.CrudTest.Application.Customers.Models;
using Mc2.CrudTest.Specs.StepDefinitions.Shared;

namespace Mc2.CrudTest.Specs.StepDefinitions
{
    [Binding]
    public partial class CustomerManagerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClientContext _httpClientContext;

        private HttpResponseMessage _createdCustomerHttpResponseMessage;

        public CustomerManagerStepDefinitions(ScenarioContext scenarioContext, HttpClientContext httpClientContext)
        {
            _scenarioContext = scenarioContext;
            _httpClientContext = httpClientContext;
        }

        [Given(@"a new customer with the following details:")]
        public void GivenANewCustomerWithTheFollowingDetails(Table table)
        {
            // Convert the table to a dictionary
            var details = table.Rows[0].ToDictionary(row => row.Key, row => row.Value);

            // Create a new customer
            var createCustomerModel = new CreateCustomerDto
            {
                FirstName = details["FirstName"],
                LastName = details["LastName"],
                DateOfBirth = DateTime.Parse(details["DateOfBirth"]),
                PhoneNumber = details["PhoneNumber"],
                BankAccountNumber = details["BankAccountNumber"],
                Email = details["Email"]
            };

            // Store the customer details in the scenario context for later use
            _scenarioContext["CreatedCustomerModel"] = createCustomerModel;
        }

        [When(@"I add the new customer to the system")]
        public async Task WhenIAddTheNewCustomerToTheSystemAsync()
        {
            // Send the POST request to the API endpoint            
            var createdCustomerModel = _scenarioContext.Get<CreateCustomerDto>("CreatedCustomerModel");
            var aCustomerJsoned = JsonContent.Create(createdCustomerModel);

            _createdCustomerHttpResponseMessage = await _httpClientContext.HttpClient.PostAsync("customer", aCustomerJsoned);
        }

        [Then(@"the customer should be added successfully")]
        public async Task ThenTheCustomerShouldBeAddedSuccessfullyAsync()
        {
            // Assert the response status code
            _createdCustomerHttpResponseMessage.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, _createdCustomerHttpResponseMessage.StatusCode);
            
            var responseObject = await _createdCustomerHttpResponseMessage.Content.ReadFromJsonAsync<CreatedResultEnvelope>();
            _scenarioContext["CreatedCustomerId"] = responseObject.Id;
        }
    }
}
