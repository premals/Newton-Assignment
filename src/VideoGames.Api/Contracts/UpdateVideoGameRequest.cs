using System.ComponentModel.DataAnnotations;

namespace VideoGames.Api.Contracts;

public sealed class UpdateVideoGameRequest
{
    [Required, StringLength(200)] public string Title { get; init; } = string.Empty;
    [Required, StringLength(50)]  public string Platform { get; init; } = string.Empty;
    [Required, StringLength(50)]  public string Genre { get; init; } = string.Empty;
    [StringLength(100)]           public string? Developer { get; init; }
    public DateOnly? ReleaseDate { get; init; }
    [Range(0, 999.99)]            public decimal Price { get; init; }
    [Range(0, 100)]               public int? Rating { get; init; }
    [Required]                    public Guid Version { get; init; }
}
