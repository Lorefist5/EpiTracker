using DefaultCoreLibrary.Core;
using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Domain.Features.Individuals.Interfaces;

public interface IIndividualRepository
{
    Task<IEnumerable<GetIndividualDto>> GetIndividualsAsync(CancellationToken cancellationToken);
    Task<GetIndividualDto?> GetIndividualByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<int>> CreateIndividualAsync(CreateIndividualDto individual, CancellationToken cancellationToken);
    Task<HttpResult<bool>> DeleteIndividualAsync(int id, CancellationToken cancellationToken);
    Task<HttpResult<bool>> UpdateIndividualAsync(int id, UpdateIndividualDto individual, CancellationToken cancellationToken);
}
