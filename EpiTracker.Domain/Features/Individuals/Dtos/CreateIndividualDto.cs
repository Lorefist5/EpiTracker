namespace EpiTracker.Domain.Features.Individuals.Dtos;

public sealed class CreateIndividualDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Symptoms { get; set; } = new();
    public DateTime? DateDiagnosed { get; set; }

    public bool IsDiagnosed => DateDiagnosed.HasValue;

    public CreateIndividualDto(string name, int age, List<string> symptoms, DateTime? dateDiagnosed)
    {
        Name = name;
        Age = age;
        Symptoms = symptoms;
        DateDiagnosed = dateDiagnosed;
    }
    public CreateIndividualDto()
    {
        
    }
}