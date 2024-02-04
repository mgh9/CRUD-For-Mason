using FluentValidation;

namespace Mc2.CrudTest.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.NewFirstName)
                .MaximumLength(50);

            RuleFor(x => x.NewLastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.NewEmail)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress().WithMessage("A valid email address is required"); // simple validation here, the main validation logic in the domain

            // we are using an external phoneValidator in domain.
            // if needed, we can use a regex or anything here, too
            RuleFor(x => x.NewPhoneNumber)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
