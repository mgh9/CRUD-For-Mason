﻿using Mc2.CrudTest.Domain.Abstractions.Models;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer : EventSourcedAggregate
    {
        public string? FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public PhoneNumber PhoneNumber { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public Email Email { get; private set; }
        public BankAccountNumber BankAccountNumber { get; private set; }
    }
}
