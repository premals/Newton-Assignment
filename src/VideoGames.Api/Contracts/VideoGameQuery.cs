using System.ComponentModel.DataAnnotations;

namespace VideoGames.Api.Contracts;

public sealed class VideoGameQuery
{
    public string? Search { get; init; }
    public string SortBy { get; init; } = "title";      // title | releaseDate | rating | price
    public bool Descending { get; init; }
    [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
    [Range(1, 100)]          public int PageSize { get; init; } = 10;
}
