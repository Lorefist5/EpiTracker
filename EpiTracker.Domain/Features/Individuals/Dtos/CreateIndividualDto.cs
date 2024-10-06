namespace EpiTracker.Domain.Features.Individuals.Dtos;

public sealed record CreateIndividualDto(string Name, int Age, IEnumerable<string> Symptoms, DateTime? DateDiagnosed)
{
    public bool IsDiagnosed => DateDiagnosed.HasValue;
}