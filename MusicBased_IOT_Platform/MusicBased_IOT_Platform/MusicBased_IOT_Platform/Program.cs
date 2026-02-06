using MusicBased_IOT_Platform.Application;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Services;
using MusicBased_IOT_Platform.Application.Services.Live;
using MusicBased_IOT_Platform.Application.Services.Mock;
using MusicBased_IOT_Platform.Components;
using MusicBased_IOT_Platform.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Demo use of the mock data service
//builder.Services.AddSingleton<IMockSpotifyDataService, LiveSpotifyDataService>();
// Create an instance of the SpotifyClientApplication and start it running
//SpotifyClientApplication spotifyClientApplication = new();
//spotifyClientApplication.Run();

//builder.Services.AddScoped<SpotifyService>();
//builder.Services.AddScoped<SpotifyService, ISpotifyService>();


builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("Spotify"));

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

app.Run();
