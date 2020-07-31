using System.Threading.Tasks;
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

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                context.ExceptionHandled = true;
                await ExceptionResponseHelper.WriteResponseForException(context.Exception, context.HttpContext, true);
            }

            await base.OnExceptionAsync(context);
        }
    }
}
