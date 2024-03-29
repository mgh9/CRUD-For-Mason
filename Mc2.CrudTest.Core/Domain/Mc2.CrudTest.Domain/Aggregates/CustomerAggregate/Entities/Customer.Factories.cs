﻿using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer
    {
        private Customer()
        {
            // for re-hydration
        }

        private Customer(Guid id, string? firstName, string lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Email email, BankAccountNumber bankAccountNumber)
        {
            AddEvent(new CustomerCreatedDomainEvent(id, firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber));
        }

        public static Customer Create(Guid id, string? firstName, string lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Email email, BankAccountNumber bankAccountNumber)
        {
            firstName = (firstName ?? string.Empty).Trim();
            lastName = lastName ?? throw new InvalidInputDataException("LastName cannot be empty or null");

            if (IsValidBirthDate(dateOfBirth))
            {
                throw new InvalidInputDataException("Invalid date of birth");
            }

            return new Customer(id, firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);
        }
    }
}
