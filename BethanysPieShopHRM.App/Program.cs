using BethanysPieShopHRM.App;
using BethanysPieShopHRM.App.Models;
using BethanysPieShopHRM.App.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// adding a http client service for DI
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7039") });

// requires the Microsoft.Extensions.Http package
// register the IEmployeeDataService
// typed approach for registering the http client

// registering regular services....
//builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client =>
//    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client =>
//    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client =>
//    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


// attaches jwt token automatically for API calls
builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client =>
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client =>
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client =>
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();



builder.Services.AddScoped<ApplicationState>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    //var Audience = builder.Configuration["Auth0:Audience"];
    //var ClientId = builder.Configuration["Auth0:ClientId"];
    //var Authority = builder.Configuration["Auth0:Authority"];

    // calling the Auht0 api by the identifier...
    options.ProviderOptions.AdditionalProviderParameters
    .Add("audience", builder.Configuration["Auth0:Audience"]);
});

await builder.Build().RunAsync();
