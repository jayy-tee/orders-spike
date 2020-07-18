using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Acme.Orders.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Api.Exceptions
{
    public static class ExceptionResponseHelper
    {
        public static async Task HandleException(HttpContext httpContext, bool includeDetails)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            await WriteResponseForException(ex, httpContext, includeDetails);
        }

        public static async Task WriteResponseForException(Exception exception, HttpContext httpContext, bool includeDetails)
        {
            if (exception != null)
            {
                var problem = GenerateProblemDetails(exception)
                    .FilterDetailsIfRequired(includeDetails)
                    .EnrichWithRequestDetails(httpContext);

                httpContext.Response.ContentType = "application/problem+json";
                httpContext.Response.StatusCode = (int)problem.Status;

                await JsonSerializer.SerializeAsync(httpContext.Response.Body, problem);
            }
        }

        private static ProblemDetails GenerateProblemDetails(Exception exception)
        {
            switch (exception)
            {
                case NotFoundException ex:
                    return GenerateNotFoundProblem(ex);
                case BusinessException ex:
                    return GenerateBadRequestProblem(ex);
                default:
                    return GenerateGenericProblem(exception);
            }
        }

        private static ProblemDetails GenerateBadRequestProblem(BusinessException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 400,
                Title = ex.Message != null ? ex.Message : "Bad Request"
            };

            // todo: add provison for errors added to business exception

            return problem;
        }

        private static ProblemDetails GenerateNotFoundProblem(NotFoundException ex)
        {
            var problem = new ProblemDetails
            {
                Status = 404,
                Title = ex.Message != null ? ex.Message : "Not Found"
            };

            return problem;
        }

        private static ProblemDetails GenerateGenericProblem(Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = ex.Message != null ? ex.Message : "An error occurred",
                Detail = ex.ToString()
            };

            return problem;
        }

        private static ProblemDetails FilterDetailsIfRequired(this ProblemDetails theDetails, bool includeDetail)
        {
            theDetails.Title = theDetails.Status < 500 || includeDetail == true ? theDetails.Title : "An error occurred";
            theDetails.Detail = includeDetail == true ? theDetails.Detail : null;

            return theDetails;
        }

        private static ProblemDetails EnrichWithRequestDetails(this ProblemDetails theProblem, HttpContext httpContext)
        {
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            theProblem.Extensions["traceId"] = traceId ?? null;
            theProblem.Instance = httpContext?.Request?.Path;
        
            return theProblem;
        }
    }
}
