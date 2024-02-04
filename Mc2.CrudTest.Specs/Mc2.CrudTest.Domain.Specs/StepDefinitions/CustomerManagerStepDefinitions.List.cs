using Mc2.CrudTest.Specs.StepDefinitions.Shared;
using Newtonsoft.Json.Linq;

namespace Mc2.CrudTest.Specs.StepDefinitions
{
    public partial class CustomerManagerStepDefinitions
    {
        [When(@"I list all customers")]
        public async Task WhenIListAllCustomersAsync()
        {
            var getCustomersListHttpResponseMessage = await _httpClientContext.HttpClient.GetAsync("customer");
            getCustomersListHttpResponseMessage.EnsureSuccessStatusCode();

            var responseObjectJson = await getCustomersListHttpResponseMessage.Content.ReadAsStringAsync();
            var customersAsJson = JArray.Parse(responseObjectJson);

            _scenarioContext["TempCustomersAsJson"] = customersAsJson;
        }

        [Then(@"I should see the added customer in the list")]
        public void ThenIShouldSeeTheAddedCustomerInTheList()
        {
            var tempCustomersAsJsonArray = _scenarioContext.Get<JArray>("TempCustomersAsJson");

            var allTheEmails = tempCustomersAsJsonArray.Select(r => r.SelectToken("email.value"));

            var result = allTheEmails.Where(r => r.Value<string>() == TestCustomer.TestCreateCustomerInstance().Email);
            Assert.True(result.Any());
        }
    }
}
