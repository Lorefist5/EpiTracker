using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Aspire.Web.Features.Individuals.ViewModels;

public sealed class IndividualDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime? DateDiagnosed { get; set; }
    public IEnumerable<string> Symptoms { get; set; } = new List<string>();

    public static IndividualDetailsViewModel FromGetIndividualDto(GetIndividualDto dto) => 
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Age = dto.Age,
            DateDiagnosed = dto.DateDiagnosed,
            Symptoms = dto.Symptoms
        };
}
