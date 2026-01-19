using FluentValidation;
using Jobby.Application.Behaviors;
using Jobby.Application.Pipelines.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jobby.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
