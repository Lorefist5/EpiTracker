using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EpiTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }

}
