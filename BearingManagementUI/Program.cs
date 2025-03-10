using System.Text.Json;
using BearingManagementUI;
using BearingManagementUI.Services;
using BearingManagementUI.Services.ApiContracts;
using BearingManagementUI.Services.AuthServices;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var json = await httpClient.GetStringAsync("appsettings.json");
var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
var baseAddress = new Uri(Environment.GetEnvironmentVariable("ApiUrl") ?? "https://test-proj-api-1.azurewebsites.net/");

builder.Services.AddAuthorizationCore();

// register the custom state provider
builder.Services.AddScoped<BrowserStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped(
    sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddRefitClient<IBearingsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = baseAddress);
builder.Services.AddRefitClient<IAuthApi>()
    .ConfigureHttpClient(c => c.BaseAddress = baseAddress);
builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();