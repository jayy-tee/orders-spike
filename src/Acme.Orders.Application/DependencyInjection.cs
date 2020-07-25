using System.Reflection;
using Acme.Orders.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Orders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
