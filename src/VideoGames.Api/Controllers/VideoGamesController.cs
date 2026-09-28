using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoGames.Api.Contracts;
using VideoGames.Api.Services;

namespace VideoGames.Api.Controllers;

[ApiController]
[Route("api/video-games")]
[Produces("application/json")]
public class VideoGamesController(IVideoGameService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResult<VideoGameDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<VideoGameDto>>> Get([FromQuery] VideoGameQuery query, CancellationToken ct) =>
        Ok(await service.GetPageAsync(query, ct));

    [HttpGet("{id:int}")]
    [ProducesResponseType<VideoGameDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VideoGameDto>> GetById(int id, CancellationToken ct) =>
        await service.GetByIdAsync(id, ct) is { } game ? Ok(game) : NotFound();

    [HttpPut("{id:int}")]
    [ProducesResponseType<VideoGameDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<VideoGameDto>> Update(int id, UpdateVideoGameRequest request, CancellationToken ct)
    {
        var (outcome, game) = await service.UpdateAsync(id, request, ct);

        return outcome switch
        {
            UpdateOutcome.Updated  => Ok(game),
            UpdateOutcome.NotFound => NotFound(),
            UpdateOutcome.Conflict => Conflict(new ProblemDetails
            {
                Title  = "Concurrency conflict",
                Detail = "This game was changed by someone else. Reload and try again.",
                Status = StatusCodes.Status409Conflict
            }),
            _ => throw new UnreachableException()
        };
    }
}
