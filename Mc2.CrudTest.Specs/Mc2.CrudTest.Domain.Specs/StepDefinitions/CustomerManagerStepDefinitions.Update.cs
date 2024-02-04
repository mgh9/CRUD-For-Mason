using System.Net;
using System.Net.Http.Json;
using Mc2.CrudTest.Application.Customers.Models;

namespace Mc2.CrudTest.Specs.StepDefinitions
{
    public partial class CustomerManagerStepDefinitions
    {
        private HttpResponseMessage _updatedCustomerHttpResponseMessage;

        [Given(@"I have added a customer to the system")]
        public void GivenIHaveAddedACustomerToTheSystem()
        {
            //
        }

        [When(@"I update the customer's email to ""([^""]*)""")]
        public async Task WhenIUpdateTheCustomersEmailToAsync(string newEmail)
        {
            // Send the PUT request to the API endpoint
            var createdCustomerId= _scenarioContext.Get<Guid>("CreatedCustomerId");
            var createdCustomer = _scenarioContext.Get<CreateCustomerDto>("CreatedCustomerModel");

            var updateCustomerModel = new UpdateCustomerDto
            {
                Id = createdCustomerId,

                NewEmail = newEmail,
                NewDateOfBirth = createdCustomer.DateOfBirth,
                NewFirstName = createdCustomer.FirstName,
                NewLastName = createdCustomer.LastName,
                NewPhoneNumber = createdCustomer.PhoneNumber,
            };

            var theCustomerJsoned = JsonContent.Create(updateCustomerModel);
            _updatedCustomerHttpResponseMessage = await _httpClientContext.HttpClient.PutAsync("customer", theCustomerJsoned);
        }

        [Then(@"the customer's email should be updated successfully")]
        public void ThenTheCustomersEmailShouldBeUpdatedSuccessfully()
        {
            // Assert the response status code
            _updatedCustomerHttpResponseMessage.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, _updatedCustomerHttpResponseMessage.StatusCode);
        }
    }
}
