using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;

public sealed record CustomerUpdatedDomainEvent(Guid Id
                                                    , string? FirstName
                                                    , string LastName
                                                    , DateTime DateOfBirth
                                                    , PhoneNumber PhoneNumber
                                                    , Email Email
                                                    /* the BankAccountNumber cannot be changed */
                                                    , DateTime UpdatedTime) : DomainEvent
{

}
