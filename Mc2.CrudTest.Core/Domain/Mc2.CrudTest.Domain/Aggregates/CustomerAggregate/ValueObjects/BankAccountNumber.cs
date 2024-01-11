﻿using CSharpFunctionalExtensions;
using IbanNet;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class BankAccountNumber : ValueObject
    {
        public string Value { get; private set; }

        private BankAccountNumber(string value)
        {
            Value = value;
        }

        public static BankAccountNumber Create(string value)
        {
            Validate(value);

            return new BankAccountNumber(value);
        }

        private static void Validate(string bankAccountNumber)
        {
            //if(string.IsNullOrWhiteSpace(value))
            //{
            //    throw new ArgumentException("Bank account number cannot be null or empty.", nameof(value));
            //}

            //if (!IsValidBankAccountNumberFormat(value))
            //{
            //    throw new ArgumentException($"Invalid bank account number format: {value}", nameof(value));
            //}

            var ibanValidator = new IbanValidator();
            var result = ibanValidator.Validate(bankAccountNumber);
            if (!result.IsValid)
            {
                throw new ArgumentException($"Invalid IBAN format: {bankAccountNumber}. {result.Error}", nameof(bankAccountNumber));
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
