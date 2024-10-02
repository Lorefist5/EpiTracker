using DefaultCoreLibrary.Core;
using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Errors;
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

    public async Task<Result<int>> CreateIndividualAsync(CreateIndividualDto individual, CancellationToken cancellationToken)
    {
        Individual newIndividual = new()
        {
            Name = individual.Name,
            Age = individual.Age,
            Symptoms = individual.Symptoms,
            DateDiagnosed = individual.DateDiagnosed,
            IsDiagnosed = individual.IsDiagnosed
        };
        try
        {
            await _context.Individuals.AddAsync(newIndividual, cancellationToken);
            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Log the exception
            return ex;
        }
        // Return the ID of the newly created individual
        return newIndividual.Id;
    }

    public async Task<HttpResult<bool>> DeleteIndividualAsync(int id, CancellationToken cancellationToken)
    {
        var individualSearchResult = await _context.Individuals.FindAsync(id);
        if (individualSearchResult is null)
            return IndividualErrors.IndividualNotFound(id);

        _context.Individuals.Remove(individualSearchResult);
        var changes = await _context.SaveChangesAsync(cancellationToken);

        return changes > 0;
    }

    public async Task<GetIndividualDto?> GetIndividualByIdAsync(int id, CancellationToken cancellationToken)
    {
        var individualSearchResult = await _context.Individuals.FindAsync(id);
        if(individualSearchResult is null)
            return null;
        
        return GetIndividualDto.FromDomainIndividual(individualSearchResult);
    }

    public async Task<IEnumerable<GetIndividualDto>> GetIndividualsAsync(CancellationToken cancellationToken)
        => await _context.Individuals
            .Select(x => GetIndividualDto.FromDomainIndividual(x))
            .ToListAsync(cancellationToken);

    public async Task<HttpResult<bool>> UpdateIndividualAsync(int id, UpdateIndividualDto individual, CancellationToken cancellationToken)
    {
        var individualSearchResult = await _context.Individuals.FindAsync(id);
        if (individualSearchResult is null)
            return IndividualErrors.IndividualNotFound(id);
        // Change the individuals properties
        individualSearchResult.Name = individual.Name;
        individualSearchResult.Age = individual.Age;
        individualSearchResult.Symptoms = individual.Symptoms;
        individualSearchResult.DateDiagnosed = individual.DateDiagnosed;
        individualSearchResult.IsDiagnosed = individual.DateDiagnosed is not null;
        
        // Finalize the update
        _context.Individuals.Update(individualSearchResult);
        try
        {
            var saveChangesAsyncResult = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return ex; // Return the exception as a new HttpResultError with status code of internal error
        }

        return true;
    }
}
