using Mc2.CrudTest.Application.Abstractions.Commands;

namespace Mc2.CrudTest.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(Guid Id
                                    , string? NewFirstName
                                    , string NewLastName
                                    , string NewPhoneNumber
                                    , string NewEmail
                                    , DateTime NewDateOfBirth) : Command;
