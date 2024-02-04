using Mc2.CrudTest.Application.Customers.Models;

namespace Mc2.CrudTest.Specs.StepDefinitions.Shared
{
    internal class TestCustomer
    {
        internal static CreateCustomerDto TestCreateCustomerInstance()
        {
            return new CreateCustomerDto
            {
                FirstName = "test_firstname",
                LastName = "test_lastname",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "+989364726673",
                BankAccountNumber = "DE03601202003749456545",
                Email = "test_customer_temp@gmail.com"
            };
        }
    }
}
