using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGames.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Version = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Developer", "Genre", "Platform", "Price", "Rating", "ReleaseDate", "Title", "Version" },
                values: new object[,]
                {
                    { 1, "Nintendo", "Adventure", "Switch", 59.99m, 97, new DateOnly(2017, 3, 3), "The Legend of Zelda: Breath of the Wild", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e01") },
                    { 2, "FromSoftware", "RPG", "PS5", 59.99m, 96, new DateOnly(2022, 2, 25), "Elden Ring", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e02") },
                    { 3, "Supergiant Games", "Roguelike", "PC", 24.99m, 93, new DateOnly(2020, 9, 17), "Hades", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e03") },
                    { 4, "ConcernedApe", "Simulation", "PC", 14.99m, 89, new DateOnly(2016, 2, 26), "Stardew Valley", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e04") },
                    { 5, "343 Industries", "Shooter", "Xbox Series X", 59.99m, 87, new DateOnly(2021, 12, 8), "Halo Infinite", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e05") },
                    { 6, "Santa Monica Studio", "Action", "PS5", 69.99m, 94, new DateOnly(2022, 11, 9), "God of War Ragnarök", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e06") },
                    { 7, "CD Projekt Red", "RPG", "PC", 39.99m, 93, new DateOnly(2015, 5, 19), "The Witcher 3: Wild Hunt", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e07") },
                    { 8, "Maddy Makes Games", "Platformer", "Switch", 19.99m, 92, new DateOnly(2018, 1, 25), "Celeste", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e08") },
                    { 9, "Playground Games", "Racing", "Xbox Series X", 59.99m, 92, new DateOnly(2021, 11, 9), "Forza Horizon 5", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e09") },
                    { 10, "Larian Studios", "RPG", "PC", 59.99m, 96, new DateOnly(2023, 8, 3), "Baldur's Gate 3", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e10") },
                    { 11, "Nintendo", "Simulation", "Switch", 59.99m, 90, new DateOnly(2020, 3, 20), "Animal Crossing: New Horizons", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e11") },
                    { 12, "CD Projekt Red", "RPG", "PC", 49.99m, 86, new DateOnly(2020, 12, 10), "Cyberpunk 2077", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e12") },
                    { 13, "Nintendo", "Platformer", "Switch", 59.99m, 97, new DateOnly(2017, 10, 27), "Super Mario Odyssey", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e13") },
                    { 14, "Capcom", "Survival Horror", "PS5", 59.99m, 93, new DateOnly(2023, 3, 24), "Resident Evil 4", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e14") },
                    { 15, "Atlus", "RPG", "PS5", 59.99m, 95, new DateOnly(2020, 3, 31), "Persona 5 Royal", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e15") },
                    { 16, "Team Cherry", "Metroidvania", "PC", 14.99m, 90, new DateOnly(2017, 2, 24), "Hollow Knight", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e16") },
                    { 17, "Blizzard Entertainment", "Shooter", "PC", 0.00m, 79, new DateOnly(2022, 10, 4), "Overwatch 2", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e17") },
                    { 18, "Blizzard Entertainment", "Action RPG", "PS5", 69.99m, 85, new DateOnly(2023, 6, 6), "Diablo IV", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e18") },
                    { 19, "MercurySteam", "Metroidvania", "Switch", 59.99m, 88, new DateOnly(2021, 10, 8), "Metroid Dread", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e19") },
                    { 20, "Capcom", "Fighting", "PS5", 59.99m, 92, new DateOnly(2023, 6, 2), "Street Fighter 6", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e20") },
                    { 21, "Square Enix", "RPG", "PS5", 69.99m, 87, new DateOnly(2023, 6, 22), "Final Fantasy XVI", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e21") },
                    { 22, "Hazelight Studios", "Adventure", "PC", 39.99m, 89, new DateOnly(2021, 3, 26), "It Takes Two", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e22") },
                    { 23, "Rare", "Adventure", "Xbox Series X", 39.99m, 81, new DateOnly(2018, 3, 20), "Sea of Thieves", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e23") },
                    { 24, "Kojima Productions", "Action", "PC", 39.99m, 82, new DateOnly(2019, 11, 8), "Death Stranding", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e24") },
                    { 25, "Insomniac Games", "Action", "PS5", 69.99m, 90, new DateOnly(2023, 10, 20), "Spider-Man 2", new Guid("7b0e6b1a-0f4e-4c64-9d7c-1a2b3c4d5e25") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_Title",
                table: "VideoGames",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
