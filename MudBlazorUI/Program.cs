using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazorUI;
using MudBlazorUI.Auth.DTOs;
using MudBlazorUI.Auth.Handler;
using MudBlazorUI.Auth.Services;
using Blazored.LocalStorage;
using MudBlazorUI.Auth.Const;
using MudBlazorUI.Notification_Service.Services;

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

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddTransient<JwtAuthenticationHandler>();
builder.Services.AddHttpClient("ServerApi", client => client.BaseAddress = new Uri("https://localhost:7111"))
                .AddHttpMessageHandler<JwtAuthenticationHandler>();

builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserProfile, UserProfile>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorageAsSingleton();
builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();
