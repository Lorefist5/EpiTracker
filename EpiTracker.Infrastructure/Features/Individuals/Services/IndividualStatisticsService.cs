using EpiTracker.Application.Features.Individuals.Interfaces;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using EpiTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EpiTracker.Infrastructure.Features.Individuals.Services;

public class IndividualStatisticsService : IIndividualStatisticsService
{
    private readonly EpiTrackerContext _epiTrackerContext;

    public IndividualStatisticsService(EpiTrackerContext epiTrackerContext)
    {
        _epiTrackerContext = epiTrackerContext;
    }

    public async Task<Dictionary<DateTime, int>> GetDiagnosisDateStatisticsAsync()
    {
        var statistics = await _epiTrackerContext.Individuals
            .Where(i => i.DateDiagnosed.HasValue) // Only consider records with a valid diagnosis date
            .GroupBy(i => i.DateDiagnosed!.Value)  // Group by DateDiagnosed
            .Select(g => new
            {
                DiagnosisDate = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .ToDictionaryAsync(g => g.DiagnosisDate, g => g.Count);

        return statistics;
    }

    public async Task<Dictionary<string, int>> GetMostCommonSymptomsAsync()
    {
        var individualsWithSymptoms = await _epiTrackerContext.Individuals
            .Where(i => i.Symptoms != null && i.Symptoms.Any()) // Filter individuals who have symptoms
            .ToListAsync(); // Fetch the individuals into memory for client-side processing

     
        var symptomsStatistics = individualsWithSymptoms
            .SelectMany(i => i.Symptoms)                        
            .GroupBy(s => s.ToLower().Trim())                   
            .Select(g => new
            {
                Symptom = g.Key,                                
                Count = g.Count()                               
            })
            .ToDictionary(g => g.Symptom, g => g.Count);        

        return symptomsStatistics.OrderBy(s => s.Value).ToDictionary();
    }


}
