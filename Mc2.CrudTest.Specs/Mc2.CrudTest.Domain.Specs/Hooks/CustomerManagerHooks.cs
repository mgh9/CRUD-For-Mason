using System.Net.Http.Json;
using Mc2.CrudTest.Api.Infrastructure.ActionResults;
using Mc2.CrudTest.Specs.StepDefinitions.Shared;

namespace Mc2.CrudTest.Specs.Hooks
{
    [Binding]
    public sealed class CustomerManagerHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClientContext _httpClientContext;

        public CustomerManagerHooks(ScenarioContext scenarioContext, HttpClientContext httpClientContext)
        {
            _scenarioContext = scenarioContext;
            _httpClientContext = httpClientContext;
        }

        [BeforeScenario("@RequireTestCustomer")]
        public async Task SetupTestCustomerAsync()
        {
            // Create a new customer
            var createCustomerModel = TestCustomer.TestCreateCustomerInstance();

            var aCustomerJsoned = JsonContent.Create(createCustomerModel);
            var response = await _httpClientContext.HttpClient.PostAsync("customer", aCustomerJsoned);
            response.EnsureSuccessStatusCode();

            var responseObject = await response.Content.ReadFromJsonAsync<CreatedResultEnvelope>();
            _scenarioContext["CreatedCustomerId"] = responseObject.Id;
            _scenarioContext["CreatedCustomerModel"] = createCustomerModel;
        }

        [AfterScenario(tags: ["@DeleteTestCustomer"])]
        public async Task DeleteTestCustomerAsync()
        {
            var createdCustomerId = _scenarioContext.Get<Guid>("CreatedCustomerId");

            var response = await _httpClientContext.HttpClient.DeleteAsync($"customer/{createdCustomerId}");
            response.EnsureSuccessStatusCode();
        }
    }
}