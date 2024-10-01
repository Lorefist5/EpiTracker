using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Exceptions;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using EpiTracker.Domain.Features.Individuals.Models;
using EpiTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EpiTracker.Infrastructure.Features.Individuals.Repositories;

public class IndividualRepository : IIndividualRepository
{
    private readonly EpiTrackerContext _context;

    public IndividualRepository(EpiTrackerContext context)
    {
        _context = context;
    }

    public async Task<int> CreateIndividualAsync(CreateIndividualDto individual, CancellationToken cancellationToken)
    {
        Individual newIndividual = new()
        {
            Name = individual.Name,
            Age = individual.Age,
            Symptoms = individual.Symptoms,
            DateDiagnosed = individual.DateDiagnosed,
            IsDiagnosed = individual.IsDiagnosed
        };

        await _context.Individuals.AddAsync(newIndividual);
        var changesResults = await _context.SaveChangesAsync(cancellationToken);
        return changesResults;
    }

    public async Task<bool> DeleteIndividualAsync(int id, CancellationToken cancellationToken)
    {
        var individualSearchResult = await _context.Individuals.FindAsync(id);
        if (individualSearchResult is null)
        {
            throw new IndividualNotFoundException(id);
        }
        _context.Individuals.Remove(individualSearchResult);

        // Save changes and get the result (number of affected entries)
        var changes = await _context.SaveChangesAsync(cancellationToken);

        // If at least one change was made (i.e., the individual was deleted), return true
        return changes > 0;
    }

    public async Task<GetIndividualDto?> GetIndividualByIdAsync(int id, CancellationToken cancellationToken)
    {
        var individualSearchResult = await _context.Individuals.FindAsync(id);
        if(individualSearchResult is null)
            return null;
        
        return GetIndividualDto.FromDomainIndividual(individualSearchResult);
    }

    public Task<List<GetIndividualDto>> GetIndividualsAsync(CancellationToken cancellationToken)
        => _context.Individuals
            .Select(x => GetIndividualDto.FromDomainIndividual(x))
            .ToListAsync(cancellationToken);
    
}
