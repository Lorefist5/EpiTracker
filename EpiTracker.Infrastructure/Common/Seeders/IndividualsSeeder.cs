using Bogus;
using EpiTracker.Domain.Features.Individuals.Models;
using EpiTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EpiTracker.Infrastructure.Common.Seeders;

public class IndividualsSeeder
{
    private readonly EpiTrackerContext _context;

    public IndividualsSeeder(EpiTrackerContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Check if individuals already exist in the database
        if (await _context.Individuals.AnyAsync()) return;

        // Define the list of possible symptoms
        var symptomsList = new List<string> { "Fever", "Cough", "Headache", "Fatigue", "Sore throat", "Shortness of breath", "Nausea" };

        // Define a couple of specific dates that should be duplicated
        var duplicateDate1 = DateTime.Now.AddDays(-10); // 10 days ago
        var duplicateDate2 = DateTime.Now.AddDays(-5);  // 5 days ago

        var faker = new Faker<Individual>()
            .RuleFor(i => i.Name, f => f.Name.FullName())                      // Generate a random full name
            .RuleFor(i => i.Age, f => f.Random.Int(1, 100))                    // Generate a random age between 1 and 100
            .RuleFor(i => i.Symptoms, f => f.PickRandom(symptomsList, f.Random.Int(1, 4)).ToList()) // Pick 1 to 4 random symptoms
            .RuleFor(i => i.IsDiagnosed, f => f.Random.Bool())                 // Randomly determine if diagnosed
            .RuleFor(i => i.DateDiagnosed, (f, i) =>
            {
                if (!i.IsDiagnosed)
                    return (DateTime?)null;

                // Randomly assign one of the predefined duplicate dates to some individuals
                var shouldUseDuplicateDate = f.Random.Int(1, 10) <= 2; // 20% chance to assign a duplicate date
                if (shouldUseDuplicateDate)
                {
                    return f.Random.Bool() ? duplicateDate1 : duplicateDate2; // Randomly pick between the two duplicate dates
                }

                // Otherwise, assign a random past date
                return f.Date.Past(1);
            });
        // Generate 500 dummy individuals
        var individuals = faker.Generate(500);

        // Add the generated individuals to the database context
        await _context.Individuals.AddRangeAsync(individuals);

        // Save the changes to the database
        await _context.SaveChangesAsync();
    }
}