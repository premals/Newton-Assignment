using VideoGames.Api.Contracts;

namespace VideoGames.Api.Services;

public enum UpdateOutcome { Updated, NotFound, Conflict }

public interface IVideoGameService
{
    Task<PagedResult<VideoGameDto>> GetPageAsync(VideoGameQuery query, CancellationToken ct = default);
    Task<VideoGameDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<(UpdateOutcome Outcome, VideoGameDto? Game)> UpdateAsync(int id, UpdateVideoGameRequest request, CancellationToken ct = default);
}
