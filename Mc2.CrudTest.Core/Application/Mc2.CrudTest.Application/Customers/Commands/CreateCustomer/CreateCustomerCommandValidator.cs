using FluentValidation;

namespace Mc2.CrudTest.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        //private readonly IPhoneNumberValidator _phoneValidator;

        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress().WithMessage("A valid email address is required");

            // we are using an external phoneValidator in domain.
            // if needed, we can use a regex or anything here, too
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty()
                .MaximumLength(32);
        }
    }
}
