using Microsoft.AspNetCore.Mvc;
using VideoGames.Api.Contracts;
using VideoGames.Api.Controllers;
using VideoGames.Api.Services;

namespace VideoGames.Api.Tests;

public class VideoGamesControllerTests
{
    private sealed class FakeService(UpdateOutcome outcome) : IVideoGameService
    {
        public Task<PagedResult<VideoGameDto>> GetPageAsync(VideoGameQuery q, CancellationToken ct = default) =>
            Task.FromResult(new PagedResult<VideoGameDto>([], 0, 1, 10));
        public Task<VideoGameDto?> GetByIdAsync(int id, CancellationToken ct = default) =>
            Task.FromResult<VideoGameDto?>(null);
        public Task<(UpdateOutcome, VideoGameDto?)> UpdateAsync(int id, UpdateVideoGameRequest r, CancellationToken ct = default) =>
            Task.FromResult<(UpdateOutcome, VideoGameDto?)>((outcome, null));
    }

    [Theory]
    [InlineData(UpdateOutcome.NotFound, typeof(NotFoundResult))]
    [InlineData(UpdateOutcome.Conflict, typeof(ConflictObjectResult))]
    public async Task Update_MapsOutcomeToHttpResult(UpdateOutcome outcome, Type expected)
    {
        var controller = new VideoGamesController(new FakeService(outcome));
        var result = await controller.Update(1, new UpdateVideoGameRequest(), CancellationToken.None);
        Assert.IsType(expected, result.Result);
    }

    [Fact]
    public async Task GetById_Returns404_WhenMissing()
    {
        var controller = new VideoGamesController(new FakeService(UpdateOutcome.Updated));
        var result = await controller.GetById(42, CancellationToken.None);
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
