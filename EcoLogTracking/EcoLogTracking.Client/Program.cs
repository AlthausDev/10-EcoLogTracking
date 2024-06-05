using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

builder.Services.AddScoped(client =>
{
    var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
    return new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
});

builder.Services.AddBlazorBootstrap();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
