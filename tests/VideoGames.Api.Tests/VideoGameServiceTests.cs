using Microsoft.Extensions.Logging.Abstractions;
using VideoGames.Api.Contracts;
using VideoGames.Api.Data;
using VideoGames.Api.Services;

namespace VideoGames.Api.Tests;

public class VideoGameServiceTests : IDisposable
{
    private readonly TestDb _db = new();
    public void Dispose() => _db.Dispose();

    private static VideoGameService CreateSut(CatalogueDbContext ctx) =>
        new(ctx, NullLogger<VideoGameService>.Instance);

    private static UpdateVideoGameRequest ValidRequest(Guid version) => new()
    {
        Title = "Elden Ring: GOTY", Platform = "PS5", Genre = "RPG",
        Developer = "FromSoftware", Price = 49.99m, Rating = 96, Version = version
    };

    [Fact]
    public async Task GetPage_FiltersBySearchTerm()
    {
        using var ctx = _db.CreateContext();
        var sut = CreateSut(ctx);

        var result = await sut.GetPageAsync(new VideoGameQuery { Search = "elden" });

        var game = Assert.Single(result.Items);
        Assert.Equal("Elden Ring", game.Title);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetPage_AppliesPagingAndReportsTotal()
    {
        using var ctx = _db.CreateContext();
        var sut = CreateSut(ctx);

        var result = await sut.GetPageAsync(new VideoGameQuery { Page = 2, PageSize = 2 });

        Assert.Equal(2, result.Items.Count);
        Assert.Equal(ctx.VideoGames.Count(), result.TotalCount);
    }

    [Fact]
    public async Task GetPage_SortsByRatingDescending()
    {
        using var ctx = _db.CreateContext();
        var sut = CreateSut(ctx);

        var result = await sut.GetPageAsync(new VideoGameQuery { SortBy = "rating", Descending = true, PageSize = 100 });

        var ratings = result.Items.Select(g => g.Rating).ToList();
        Assert.Equal(ratings.OrderByDescending(r => r), ratings);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenMissing()
    {
        using var ctx = _db.CreateContext();
        Assert.Null(await CreateSut(ctx).GetByIdAsync(9999));
    }

    [Fact]
    public async Task Update_PersistsChanges_AndRotatesVersion()
    {
        Guid originalVersion;
        using (var ctx = _db.CreateContext())
            originalVersion = (await CreateSut(ctx).GetByIdAsync(2))!.Version;

        using (var ctx = _db.CreateContext())
        {
            var (outcome, game) = await CreateSut(ctx).UpdateAsync(2, ValidRequest(originalVersion));
            Assert.Equal(UpdateOutcome.Updated, outcome);
            Assert.NotEqual(originalVersion, game!.Version);
        }

        using (var ctx = _db.CreateContext())
            Assert.Equal("Elden Ring: GOTY", ctx.VideoGames.Single(g => g.Id == 2).Title);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_ForUnknownId()
    {
        using var ctx = _db.CreateContext();
        var (outcome, _) = await CreateSut(ctx).UpdateAsync(9999, ValidRequest(Guid.NewGuid()));
        Assert.Equal(UpdateOutcome.NotFound, outcome);
    }

    [Fact]
    public async Task Update_ReturnsConflict_WhenVersionIsStale()
    {
        using var ctx = _db.CreateContext();
        var (outcome, _) = await CreateSut(ctx).UpdateAsync(2, ValidRequest(Guid.NewGuid()));
        Assert.Equal(UpdateOutcome.Conflict, outcome);
    }
}
