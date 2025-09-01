using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Behaviors;
using System.Reflection;

namespace PropertyManager.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
