namespace EpiTracker.Application.Features.Individuals.Queries.GetMostCommonSymptoms;

public record GetMostCommonSymptomsQueryResponse(Dictionary<string, int> SymptomsStatistics);