using EpiTracker.Domain.Features.Individuals.Models;
using Microsoft.EntityFrameworkCore;

namespace EpiTracker.Infrastructure.Common.Persistence;

public class EpiTrackerContext : DbContext
{
    public DbSet<Individual> Individuals { get; set; } = default!;
    public EpiTrackerContext(DbContextOptions<EpiTrackerContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Individual>(individualModelBuilder =>
        {
            individualModelBuilder.HasKey(x => x.Id);
            individualModelBuilder.Property(x => x.Name).IsRequired();
            individualModelBuilder.Property(x => x.Age).IsRequired();
            individualModelBuilder.Property(x => x.IsDiagnosed).IsRequired();
            individualModelBuilder.Property(x => x.DateDiagnosed).IsRequired(false);
        });
    }
}
