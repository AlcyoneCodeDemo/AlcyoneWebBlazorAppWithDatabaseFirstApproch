using BlazorClient;
using BlazorClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Set the root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient
var baseAddress = "https://localhost:7019/"; // Ensure this is the correct base address for your API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

// Register CustomAuthenticationStateProvider
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddTransient<AuthenticationMessageHandler>();
builder.Services.AddHttpClient("Apiclient", client =>
{
    client.BaseAddress = new Uri(baseAddress);
}).AddHttpMessageHandler<AuthenticationMessageHandler>();


await builder.Build().RunAsync();
