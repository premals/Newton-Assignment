namespace VideoGames.Api.Domain;

public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string? Developer { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int? Rating { get; set; }          // 0–100 critic score

    /// <summary>Optimistic concurrency token. Regenerated on every update.</summary>
    public Guid Version { get; set; }
}
