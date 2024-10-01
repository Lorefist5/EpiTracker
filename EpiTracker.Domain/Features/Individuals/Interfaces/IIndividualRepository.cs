using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Domain.Features.Individuals.Interfaces;

public interface IIndividualRepository
{
    Task<List<GetIndividualDto>> GetIndividualsAsync(CancellationToken cancellationToken);
    Task<GetIndividualDto?> GetIndividualByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateIndividualAsync(CreateIndividualDto individual, CancellationToken cancellationToken);
    Task<bool> DeleteIndividualAsync(int id, CancellationToken cancellationToken);
}
