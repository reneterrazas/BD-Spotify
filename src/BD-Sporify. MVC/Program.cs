using MySqlConnector;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

// Registrar IDbConnection
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

// MVC
builder.Services.AddControllersWithViews();

// Repositorios
builder.Services.AddScoped<IRepoArtistaAsync, RepoArtistaAsync>();
builder.Services.AddScoped<IRepoGeneroAsync, RepoGeneroAsync>();
builder.Services.AddScoped<IRepoAlbumAsync, RepoAlbumAsync>();
builder.Services.AddScoped<IRepoCancionAsync, RepoCancionAsync>();
builder.Services.AddScoped<IRepoNacionalidadAsync, RepoNacionalidadAsync>();
builder.Services.AddScoped<IRepoUsuarioAsinc, RepoUsuarioAsync>();

// 🔥 Agregar SESSION (Necesario para Login por Session)
builder.Services.AddSession();

// Login con Cookies (si lo vas a usar)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.AccessDeniedPath = "/Usuario/Denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

var app = builder.Build();

// Errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 IMPORTANTE: Session SIEMPRE ANTES DE Authentication
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
