using Acme.Orders.Api.Exceptions;
using Acme.Orders.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Acme.Orders.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public ApiExceptionFilter()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                context.ExceptionHandled = true;
                ExceptionResponseHelper.WriteResponseForException(context.Exception, context.HttpContext, true).GetAwaiter().GetResult();
            }

            base.OnException(context);
        }
    }
}
