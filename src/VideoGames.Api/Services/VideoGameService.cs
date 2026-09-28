using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Contracts;
using VideoGames.Api.Data;
using VideoGames.Api.Domain;

namespace VideoGames.Api.Services;

public class VideoGameService(CatalogueDbContext db, ILogger<VideoGameService> logger) : IVideoGameService
{
    // Single projection shared by every read, so EF translates it straight to SQL.
    private static readonly Expression<Func<VideoGame, VideoGameDto>> ToDto = g =>
        new VideoGameDto(g.Id, g.Title, g.Platform, g.Genre, g.Developer,
                         g.ReleaseDate, g.Price, g.Rating, g.Version);

    public async Task<PagedResult<VideoGameDto>> GetPageAsync(VideoGameQuery query, CancellationToken ct = default)
    {
        IQueryable<VideoGame> games = db.VideoGames.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            // EF.Functions.Like is case-insensitive on both SQL Server (default collation) and SQLite,
            // unlike string.Contains(), which SQLite translates to a case-sensitive instr().
            var pattern = $"%{query.Search.Trim()}%";
            games = games.Where(g => EF.Functions.Like(g.Title, pattern)
                                  || EF.Functions.Like(g.Platform, pattern)
                                  || EF.Functions.Like(g.Genre, pattern));
        }

        var total = await games.CountAsync(ct);

        var items = await ApplySort(games, query.SortBy, query.Descending)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(ToDto)
            .ToListAsync(ct);

        return new PagedResult<VideoGameDto>(items, total, query.Page, query.PageSize);
    }

    public Task<VideoGameDto?> GetByIdAsync(int id, CancellationToken ct = default) =>
        db.VideoGames.AsNoTracking()
          .Where(g => g.Id == id)
          .Select(ToDto)
          .SingleOrDefaultAsync(ct);

    public async Task<(UpdateOutcome, VideoGameDto?)> UpdateAsync(int id, UpdateVideoGameRequest request, CancellationToken ct = default)
    {
        var game = await db.VideoGames.FindAsync([id], ct);
        if (game is null)
        {
            logger.LogWarning("Update failed: game {GameId} not found.", id);
            return (UpdateOutcome.NotFound, null);
        }

        // Compare against the version the client last saw, not the one just loaded.
        db.Entry(game).Property(g => g.Version).OriginalValue = request.Version;

        game.Title       = request.Title.Trim();
        game.Platform    = request.Platform.Trim();
        game.Genre       = request.Genre.Trim();
        game.Developer   = request.Developer?.Trim();
        game.ReleaseDate = request.ReleaseDate;
        game.Price       = request.Price;
        game.Rating      = request.Rating;
        game.Version     = Guid.NewGuid();

        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException)
        {
            logger.LogWarning(
                "Concurrency conflict updating game {GameId}: client's version {ClientVersion} is stale.",
                id, request.Version);
            return (UpdateOutcome.Conflict, null);
        }

        logger.LogInformation("Game {GameId} updated. New version {Version}.", id, game.Version);
        return (UpdateOutcome.Updated, ToDto.Compile()(game));
    }

    private static IOrderedQueryable<VideoGame> ApplySort(IQueryable<VideoGame> q, string sortBy, bool desc) =>
        sortBy.ToLowerInvariant() switch
        {
            "releasedate" => desc ? q.OrderByDescending(g => g.ReleaseDate) : q.OrderBy(g => g.ReleaseDate),
            "rating"      => desc ? q.OrderByDescending(g => g.Rating)      : q.OrderBy(g => g.Rating),
            "price"       => desc ? q.OrderByDescending(g => g.Price)       : q.OrderBy(g => g.Price),
            _             => desc ? q.OrderByDescending(g => g.Title)       : q.OrderBy(g => g.Title),
        };
}
