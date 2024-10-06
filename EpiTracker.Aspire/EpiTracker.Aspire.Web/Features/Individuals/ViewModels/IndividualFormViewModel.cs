using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Aspire.Web.Features.Individuals.ViewModels;

public class IndividualFormViewModel
{
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public List<string> Symptoms { get; set; } = new();
    public DateTime? DateDiagnosed { get; set; }

    public bool IsDiagnosed => DateDiagnosed.HasValue;

    public static CreateIndividualDto ToCreateIndividualDto(IndividualFormViewModel individualFormViewModel) =>
        new CreateIndividualDto(individualFormViewModel.Name, individualFormViewModel.Age, individualFormViewModel.Symptoms, individualFormViewModel.DateDiagnosed);
    public static UpdateIndividualDto ToUpdateIndividualDto(IndividualFormViewModel individualFormViewModel) =>
        new UpdateIndividualDto(individualFormViewModel.Name, individualFormViewModel.Age, individualFormViewModel.Symptoms, individualFormViewModel.DateDiagnosed);
}
