using System.Net;

namespace Mc2.CrudTest.Specs.StepDefinitions
{
    public partial class CustomerManagerStepDefinitions
    {        
        private HttpResponseMessage _deletedCustomerHttpResponseMessage;

        [When(@"I delete the customer from the system")]
        public async Task WhenIDeleteTheCustomerFromTheSystemAsync()
        {
            // Send the dELETE request to the API endpoint
            var createdCustomerId = _scenarioContext.Get<Guid>("CreatedCustomerId");
            _deletedCustomerHttpResponseMessage = await _httpClientContext.HttpClient.DeleteAsync($"customer/{createdCustomerId}");
        }

        [Then(@"the customer should be deleted successfully")]
        public void ThenTheCustomerShouldBeDeletedSuccessfully()
        {
            // Assert the response status code
            _deletedCustomerHttpResponseMessage.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, _deletedCustomerHttpResponseMessage.StatusCode);
        }
    }
}
