using BlazorApp1.Services.Articles;
using BlazorApp1.Services.Notifications;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// Configure HttpClient with a proper BaseAddress
// Adjust the BaseAddress according to your API endpoint or environment
var httpHandler = new HttpClientHandler()
{
    AllowAutoRedirect = true,
    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
};
builder.Services.AddSingleton(sp => new HttpClient(httpHandler)
{
    BaseAddress = new Uri("https://localhost:7064/"),
    // Set a default base URL or configure as needed
});


builder.Services.AddMudServices();
builder.Services.AddHotKeys();
builder.Services.AddBlazoredLocalStorage();
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

// Register your custom services
builder.Services.AddTransient<INotificationsService, NotificationsService>();
builder.Services.AddTransient<IArticlesService, ArticlesService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
