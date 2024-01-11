using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Infrastructure.ActionResults
{
    public class EnvelopeObjectResult : ObjectResult
    {
        public EnvelopeObjectResult(Envelope envelope)
            : base(envelope)
        {
            StatusCode = envelope.Status;
        }
    }
}
