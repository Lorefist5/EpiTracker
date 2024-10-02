namespace EpiTracker.Domain.Features.Individuals.Dtos;

public record UpdateIndividualDto(string Name, int Age, List<string> Symptoms, DateTime? DateDiagnosed);