using Mc2.CrudTest.Application.Abstractions.Commands;

namespace Mc2.CrudTest.Application.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(Guid Id
                                        , string? FirstName
                                        , string LastName
                                        , string PhoneNumber
                                        , string Email
                                        , DateTime DateOfBirth
                                        , string BankAccountNumber) : Command;
}
