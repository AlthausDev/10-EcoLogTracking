using Blazored.LocalStorage;
using EcoLogTracking.Client.Services;
using EcoLogTracking.Server.Components;
using EcoLogTracking.Server.Repository.Impl;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Impl;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

#region Añadir servicios
builder.Services.AddSingleton<MockLogService>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

#endregion

#region Configuración de CORS
builder.Services.AddCors(options => options.AddPolicy("corsPolicy", builder =>
{
    _ = builder.AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowAnyOrigin();

}));




#endregion

#region Configura componentes de Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(client => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7216/"),
});
#endregion

#region Configuración de HttpClient
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new ArgumentNullException(nameof(apiBaseUrl), "ApiSettings:BaseUrl no está configurado correctamente en appsettings.json");
}

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});
#endregion


#region Configura repositorios y servicios

builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<ILogService, LogService>();
#endregion

var app = builder.Build();

#region Configuración del entorno
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    _ = app.UseExceptionHandler("/Error", createScopeForErrors: true);
    _ = app.UseHsts();
}
#endregion

#region Configuración de middleware
app.UseCors("corsPolicy");
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    logger.Info($"Handling request: {context.Request.Method} {context.Request.Path}");
//    await next.Invoke();
//    logger.Info($"Finished handling request: {context.Request.Method} {context.Request.Path}");
//});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseStaticFiles();
app.UseAntiforgery();
#endregion

#region Mapea páginas y controladores
app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EcoLogTracking.Client._Imports).Assembly);
#endregion

app.Run();
