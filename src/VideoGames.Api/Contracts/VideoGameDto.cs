namespace VideoGames.Api.Contracts;

public sealed record VideoGameDto(
    int Id, string Title, string Platform, string Genre, string? Developer,
    DateOnly? ReleaseDate, decimal Price, int? Rating, Guid Version);
