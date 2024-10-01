namespace EpiTracker.Domain.Features.Individuals.Dtos;

public record CreateIndividualDto(string Name, int Age, List<string> Symptoms, DateTime? DateDiagnosed)
{
    public bool IsDiagnosed => DateDiagnosed.HasValue;
};
