using EpiTracker.Domain.Features.Individuals.Dtos;
using Refit;

namespace EpiTracker.Aspire.Web.Features.Individuals.Services;

public interface IIndividualApi
{
    [Get("/api/individuals")]
    Task<IEnumerable<GetIndividualDto>> GetIndividualsAsync(CancellationToken cancellationToken = default);

    [Get("/api/individuals/{userId}")]
    Task<GetIndividualDto> GetIndividualByIdAsync(int userId, CancellationToken cancellationToken = default);

    [Post("/api/individuals")]
    Task<HttpResponseMessage> CreateIndividualAsync([Body] CreateIndividualDto creationRequest, CancellationToken cancellationToken = default);

    [Delete("/api/individuals/{userId}")]
    Task<HttpResponseMessage> DeleteIndividualByIdAsync(int userId, CancellationToken cancellationToken = default);

    [Put("/api/individuals/{userId}")]
    Task<HttpResponseMessage> UpdateIndividualByIdAsync(int userId, [Body] UpdateIndividualDto updateRequest, CancellationToken cancellationToken = default);
}