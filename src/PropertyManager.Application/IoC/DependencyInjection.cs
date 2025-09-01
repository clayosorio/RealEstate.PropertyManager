using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Application.Commons.Behaviors;
using System.Reflection;

namespace PropertyManager.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
