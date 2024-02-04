using System.Net;
using Mc2.CrudTest.Api.Infrastructure.ActionResults;
using Mc2.CrudTest.Domain.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.EventSourcing.Api.Infrastructure.Filters
{
    public sealed class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            Envelope envelope;

            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    envelope = Envelope.Create(notFoundException.Message, HttpStatusCode.NotFound);
                    break;

                case UnauthorizedAccessException unauthorizedAccessException:
                    envelope = Envelope.Create(unauthorizedAccessException.Message, HttpStatusCode.Forbidden);
                    //envelope = Envelope.Create("Access Denied", HttpStatusCode.Unauthorized);
                    break;

                case DomainException domainException:
                    envelope = Envelope.Create(domainException.Message, HttpStatusCode.BadRequest);
                    break;

                default:
                    string message = FetchExceptionMessage(context);
                    envelope = Envelope.Create(message, HttpStatusCode.InternalServerError);
                    break;
            }

            context.Result = envelope.ToActionResult();
            context.HttpContext.Response.StatusCode = envelope.Status;
            context.ExceptionHandled = true;
        }

        private string FetchExceptionMessage(ExceptionContext context)
        {
            return _env.IsDevelopment()
                ? context.Exception.ToString() 
                : "Sorry an error occurred, please try again.";
        }
    }
}
