using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using EpiTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using EpiTracker.Infrastructure.Features.Individuals.Repositories;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using EpiTracker.Application.Features.Individuals.Interfaces;
using EpiTracker.Infrastructure.Features.Individuals.Services;
using EpiTracker.Infrastructure.Common.Seeders;
namespace EpiTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<EpiTrackerContext>(options =>
            options.UseInMemoryDatabase("EpiTrackerDatabase"));

        services.AddScoped<IIndividualRepository, IndividualRepository>();
        services.AddScoped<IIndividualStatisticsService, IndividualStatisticsService>();

        services.AddScoped<IndividualsSeeder>();
        return services;
    }

}
