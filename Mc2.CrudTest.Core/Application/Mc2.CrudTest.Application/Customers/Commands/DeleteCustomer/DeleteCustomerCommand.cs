using Mc2.CrudTest.Application.Abstractions.Commands;

namespace Mc2.CrudTest.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand (Guid Id) : Command;