namespace EpiTracker.Domain.Features.Individuals.Models;
public class Individual
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public List<string> Symptoms { get; set; } = new List<string>();
    public bool IsDiagnosed { get; set; }
    public DateTime? DateDiagnosed { get; set; }

}
