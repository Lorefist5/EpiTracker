using EpiTracker.Domain.Features.Individuals.Models;

namespace EpiTracker.Domain.Features.Individuals.Dtos;

public record GetIndividualDto(int Id, string Name,int Age, IEnumerable<string> Symptoms, DateTime? DateDiagnosed)
{
    public bool IsDiagnosed => DateDiagnosed.HasValue;


    public static GetIndividualDto FromDomainIndividual(Individual individual) => 
        new GetIndividualDto(individual.Id, individual.Name, individual.Age, individual.Symptoms, individual.DateDiagnosed);
    
};
