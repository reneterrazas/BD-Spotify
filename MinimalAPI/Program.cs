using System.Data;
using Scalar.AspNetCore;
using Spotify.Core;
using MySqlConnector;
using Spotify.ReposDapper;
using Spotify.Core.Persistencia;
var builder = WebApplication.CreateBuilder(args);
//Obtener la cadena de conexion desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));
builder.Services.AddScoped<IRepoAlbum, RepoAlbum>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
app.MapGet("/artistas", async (IDbConnection db) =>
{
    var repo = new RepoArtistaAsync(db);
    var artistas = await repo.Obtener();
    return Results.Ok(artistas);
});

app.MapGet("/artistas/{id}", async (uint id, IDbConnection db) =>
{
    var repo = new RepoArtistaAsync(db);
    var artista = await repo.DetalleDe(id);
    return artista is not null ? Results.Ok(artista) : Results.NotFound();
});

app.MapPost("/artistas", async (Artista artista, IDbConnection db) =>
{
    var repo = new RepoArtistaAsync(db);
    var id = await repo.Alta(artista);
    return Results.Created($"/artistas/{id}", artista);
});
