using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGames.Api.Domain;

namespace VideoGames.Api.Data.Configurations;

public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> b)
    {
        b.ToTable("VideoGames");
        b.HasKey(g => g.Id);

        b.Property(g => g.Title).HasMaxLength(200).IsRequired();
        b.Property(g => g.Platform).HasMaxLength(50).IsRequired();
        b.Property(g => g.Genre).HasMaxLength(50).IsRequired();
        b.Property(g => g.Developer).HasMaxLength(100);
        b.Property(g => g.Price).HasPrecision(10, 2);
        b.Property(g => g.Version).IsConcurrencyToken();

        b.HasIndex(g => g.Title);

        b.HasData(
            new VideoGame { Id = 1, Title = "The Legend of Zelda: Breath of the Wild", Platform = "Switch", Genre = "Adventure", Developer = "Nintendo", ReleaseDate = new DateOnly(2017, 3, 3), Price = 59.99m, Rating = 97, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e01") },
            new VideoGame { Id = 2, Title = "Elden Ring", Platform = "PS5", Genre = "RPG", Developer = "FromSoftware", ReleaseDate = new DateOnly(2022, 2, 25), Price = 59.99m, Rating = 96, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e02") },
            new VideoGame { Id = 3, Title = "Hades", Platform = "PC", Genre = "Roguelike", Developer = "Supergiant Games", ReleaseDate = new DateOnly(2020, 9, 17), Price = 24.99m, Rating = 93, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e03") },
            new VideoGame { Id = 4, Title = "Stardew Valley", Platform = "PC", Genre = "Simulation", Developer = "ConcernedApe", ReleaseDate = new DateOnly(2016, 2, 26), Price = 14.99m, Rating = 89, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e04") },
            new VideoGame { Id = 5, Title = "Halo Infinite", Platform = "Xbox Series X", Genre = "Shooter", Developer = "343 Industries", ReleaseDate = new DateOnly(2021, 12, 8), Price = 59.99m, Rating = 87, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e05") },
            new VideoGame { Id = 6, Title = "God of War Ragnarök", Platform = "PS5", Genre = "Action", Developer = "Santa Monica Studio", ReleaseDate = new DateOnly(2022, 11, 9), Price = 69.99m, Rating = 94, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e06") },
            new VideoGame { Id = 7, Title = "The Witcher 3: Wild Hunt", Platform = "PC", Genre = "RPG", Developer = "CD Projekt Red", ReleaseDate = new DateOnly(2015, 5, 19), Price = 39.99m, Rating = 93, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e07") },
            new VideoGame { Id = 8, Title = "Celeste", Platform = "Switch", Genre = "Platformer", Developer = "Maddy Makes Games", ReleaseDate = new DateOnly(2018, 1, 25), Price = 19.99m, Rating = 92, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e08") },
            new VideoGame { Id = 9, Title = "Forza Horizon 5", Platform = "Xbox Series X", Genre = "Racing", Developer = "Playground Games", ReleaseDate = new DateOnly(2021, 11, 9), Price = 59.99m, Rating = 92, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e09") },
            new VideoGame { Id = 10, Title = "Baldur's Gate 3", Platform = "PC", Genre = "RPG", Developer = "Larian Studios", ReleaseDate = new DateOnly(2023, 8, 3), Price = 59.99m, Rating = 96, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e10") },
            new VideoGame { Id = 11, Title = "Animal Crossing: New Horizons", Platform = "Switch", Genre = "Simulation", Developer = "Nintendo", ReleaseDate = new DateOnly(2020, 3, 20), Price = 59.99m, Rating = 90, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e11") },
            new VideoGame { Id = 12, Title = "Cyberpunk 2077", Platform = "PC", Genre = "RPG", Developer = "CD Projekt Red", ReleaseDate = new DateOnly(2020, 12, 10), Price = 49.99m, Rating = 86, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e12") },
            new VideoGame { Id = 13, Title = "Super Mario Odyssey", Platform = "Switch", Genre = "Platformer", Developer = "Nintendo", ReleaseDate = new DateOnly(2017, 10, 27), Price = 59.99m, Rating = 97, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e13") },
            new VideoGame { Id = 14, Title = "Resident Evil 4", Platform = "PS5", Genre = "Survival Horror", Developer = "Capcom", ReleaseDate = new DateOnly(2023, 3, 24), Price = 59.99m, Rating = 93, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e14") },
            new VideoGame { Id = 15, Title = "Persona 5 Royal", Platform = "PS5", Genre = "RPG", Developer = "Atlus", ReleaseDate = new DateOnly(2020, 3, 31), Price = 59.99m, Rating = 95, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e15") },
            new VideoGame { Id = 16, Title = "Hollow Knight", Platform = "PC", Genre = "Metroidvania", Developer = "Team Cherry", ReleaseDate = new DateOnly(2017, 2, 24), Price = 14.99m, Rating = 90, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e16") },
            new VideoGame { Id = 17, Title = "Overwatch 2", Platform = "PC", Genre = "Shooter", Developer = "Blizzard Entertainment", ReleaseDate = new DateOnly(2022, 10, 4), Price = 0.00m, Rating = 79, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e17") },
            new VideoGame { Id = 18, Title = "Diablo IV", Platform = "PS5", Genre = "Action RPG", Developer = "Blizzard Entertainment", ReleaseDate = new DateOnly(2023, 6, 6), Price = 69.99m, Rating = 85, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e18") },
            new VideoGame { Id = 19, Title = "Metroid Dread", Platform = "Switch", Genre = "Metroidvania", Developer = "MercurySteam", ReleaseDate = new DateOnly(2021, 10, 8), Price = 59.99m, Rating = 88, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e19") },
            new VideoGame { Id = 20, Title = "Street Fighter 6", Platform = "PS5", Genre = "Fighting", Developer = "Capcom", ReleaseDate = new DateOnly(2023, 6, 2), Price = 59.99m, Rating = 92, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e20") },
            new VideoGame { Id = 21, Title = "Final Fantasy XVI", Platform = "PS5", Genre = "RPG", Developer = "Square Enix", ReleaseDate = new DateOnly(2023, 6, 22), Price = 69.99m, Rating = 87, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e21") },
            new VideoGame { Id = 22, Title = "It Takes Two", Platform = "PC", Genre = "Adventure", Developer = "Hazelight Studios", ReleaseDate = new DateOnly(2021, 3, 26), Price = 39.99m, Rating = 89, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e22") },
            new VideoGame { Id = 23, Title = "Sea of Thieves", Platform = "Xbox Series X", Genre = "Adventure", Developer = "Rare", ReleaseDate = new DateOnly(2018, 3, 20), Price = 39.99m, Rating = 81, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e23") },
            new VideoGame { Id = 24, Title = "Death Stranding", Platform = "PC", Genre = "Action", Developer = "Kojima Productions", ReleaseDate = new DateOnly(2019, 11, 8), Price = 39.99m, Rating = 82, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e24") },
            new VideoGame { Id = 25, Title = "Spider-Man 2", Platform = "PS5", Genre = "Action", Developer = "Insomniac Games", ReleaseDate = new DateOnly(2023, 10, 20), Price = 69.99m, Rating = 90, Version = Guid.Parse("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e25") }
        );
    }
}
