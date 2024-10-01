namespace EpiTracker.Application.Features.Individuals.Queries.GetDiagnosisDateStatistics;

public record GetDiagnosisDateStatisticsResponse(Dictionary<DateTime, int> DiagnosisDateStatistics);
