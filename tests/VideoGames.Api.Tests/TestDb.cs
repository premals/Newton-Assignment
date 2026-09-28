using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Data;

namespace VideoGames.Api.Tests;

public sealed class TestDb : IDisposable
{
    private readonly SqliteConnection _connection = new("DataSource=:memory:");

    public TestDb()
    {
        _connection.Open();
        using var ctx = CreateContext();
        ctx.Database.EnsureCreated();          // applies HasData seed
    }

    public CatalogueDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<CatalogueDbContext>().UseSqlite(_connection).Options);

    public void Dispose() => _connection.Dispose();
}
