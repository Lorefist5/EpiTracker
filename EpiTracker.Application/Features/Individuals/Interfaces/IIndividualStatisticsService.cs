namespace EpiTracker.Application.Features.Individuals.Interfaces;

public interface IIndividualStatisticsService
{
    Task<Dictionary<DateTime, int>> GetDiagnosisDateStatisticsAsync();
    Task<Dictionary<string, int>> GetMostCommonSymptomsAsync();
}
