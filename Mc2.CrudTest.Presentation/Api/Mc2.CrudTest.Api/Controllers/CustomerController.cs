using Mc2.CrudTest.Api.Infrastructure.ActionResults;
using Mc2.CrudTest.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Application.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Application.Customers.Models;
using Mc2.CrudTest.Application.Customers.Queries.GetCustomer;
using Mc2.CrudTest.Application.Customers.Queries.GetCustomers;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomerController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await _mediator.Send(new GetCustomerQuery(id), cancellationToken);
            return Ok(customer);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerReadModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            var customer = await _mediator.Send(new GetCustomersQuery(), cancellationToken);
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerDto customer, CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            await _mediator.Send(new CreateCustomerCommand(id
                                                            , customer.FirstName
                                                            , customer.LastName
                                                            , customer.PhoneNumber
                                                            , customer.Email
                                                            , customer.DateOfBirth
                                                            , customer.BankAccountNumber), cancellationToken);

            return CreatedAtAction("Post", new { id }, new CreatedResultEnvelope(id));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCustomerDto request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new UpdateCustomerCommand(request.Id
                                                            , request.NewFirstName
                                                            , request.NewLastName
                                                            , request.NewPhoneNumber
                                                            , request.NewEmail
                                                            , request.NewDateOfBirth)
                                                , cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCustomerCommand(id));

            return NoContent();
        }
    }
}
