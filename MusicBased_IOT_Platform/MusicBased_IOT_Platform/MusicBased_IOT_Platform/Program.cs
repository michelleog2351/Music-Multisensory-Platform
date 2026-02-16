using Microsoft.EntityFrameworkCore;
using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Application.Services.Spotify.Live;
using MusicBased_IOT_Platform.Application.Services.Spotify.Mock;
using MusicBased_IOT_Platform.Components;
using MusicBased_IOT_Platform.Data;
using MusicBased_IOT_Platform.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

bool useMock = builder.Configuration.GetValue<bool>("Spotify:UseMockSpotify");

if (useMock)
{
    builder.Services.AddScoped<ISpotifyDataService, MockSpotifyDataService>();
}
else
{
    builder.Services.AddHttpClient<ISpotifyDataService, LiveSpotifyDataService>();
}

var settings = builder.Configuration
    .GetSection("Spotify")
    .Get<AppSettings>();

if (settings!.InTest)
{
    builder.Services.AddScoped<ISpotifyDataService, MockSpotifyDataService>();
}
else
{
    builder.Services.AddHttpClient<ISpotifyDataService, LiveSpotifyDataService>();
}


builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MusicBased_IOT_Platform.Client._Imports).Assembly);

await app.RunAsync();
