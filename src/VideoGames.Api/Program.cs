using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Data;
using VideoGames.Api.Middleware;
using VideoGames.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogueDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Catalogue")));

builder.Services.AddScoped<IVideoGameService, VideoGameService>();
builder.Services.AddControllers();

// GlobalExceptionHandler does the logging and ProblemDetails writing; AddProblemDetails()
// just supplies the (traceId-enriched) shape it and the framework's own 4xx responses use.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                        // /openapi/v1.json

    // Convenience for the demo: apply pending migrations on startup.
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<CatalogueDbContext>().Database.Migrate();
}

app.UseExceptionHandler();          // must run first so it can catch everything downstream
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseCors();
app.MapControllers();
app.Run();

public partial class Program;                // lets integration tests use WebApplicationFactory<Program>
