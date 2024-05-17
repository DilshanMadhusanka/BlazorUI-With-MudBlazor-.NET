using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazorUI;

using MudBlazor.Services;
using MudBlazorUI.Auth.DTOs;
using MudBlazorUI.Auth.Handler;
using Blazored.SessionStorage;
using MudBlazor;





var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddTransient<JwtAuthenticationHandler>();
builder.Services.AddHttpClient("ServerApi", client => client.BaseAddress = new Uri("http://localhost:5261"))
                .AddHttpMessageHandler<JwtAuthenticationHandler>();
builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>();
builder.Services.AddBlazoredSessionStorageAsSingleton();
await builder.Build().RunAsync();
