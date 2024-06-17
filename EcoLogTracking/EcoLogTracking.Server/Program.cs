using Blazored.LocalStorage;
using EcoLogTracking.Client.Services;
using EcoLogTracking.Server.Components;
using EcoLogTracking.Server.Repository.Impl;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Impl;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text;
using todoAPI.Helpers;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();


#region Configuración de servicios

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Registrar servicios
builder.Services.AddTransient<EncryptionHelper>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
builder.Services.AddSingleton<MockLogService>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"])),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

var app = builder.Build();

#region Configuración del entorno

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    _ = app.UseExceptionHandler("/Error");
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

app.Use(async (context, next) =>
{
    logger.Info($"Handling request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    logger.Info($"Finished handling request: {context.Request.Method} {context.Request.Path}");
});

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