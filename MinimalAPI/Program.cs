using Scalar.AspNetCore;
using System.Data;
using MySqlConnector;
using Spotify.Core;
using Spotify.ReposDapper;
using Spotify.Core.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySQL");

// Registrar IDbConnection para inyección de dependencias
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints para Artista
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

// Endpoints para Album
app.MapGet("/albums", async (IDbConnection db) =>
{
    var repo = new RepoAlbumAsync(db);
    var albums = await repo.Obtener();
    return Results.Ok(albums);
});

app.MapGet("/albums/{id}", async (uint id, IDbConnection db) =>
{
    var repo = new RepoAlbumAsync(db);
    var album = await repo.DetalleDe(id);
    return album is not null ? Results.Ok(album) : Results.NotFound();
});

app.MapPost("/albums", async (Album album, IDbConnection db) =>
{
    var repo = new RepoAlbumAsync(db);
    var id = await repo.Alta(album);
    return Results.Created($"/albums/{id}", album);
});

app.Run();