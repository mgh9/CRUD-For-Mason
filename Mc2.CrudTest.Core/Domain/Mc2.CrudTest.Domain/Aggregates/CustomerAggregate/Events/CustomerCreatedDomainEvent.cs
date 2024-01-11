using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events
{
    public sealed record CustomerCreatedDomainEvent(Guid Id
                                                        , string? FirstName
                                                        , string LastName
                                                        , DateTime DateOfBirth
                                                        , PhoneNumber PhoneNumber
                                                        , Email Email
                                                        , BankAccountNumber BankAccountNumber) : DomainEvent
    {

    }
}
