using API.Middleware;
using Core.Entities;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);



builder.Services.AddCors();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>();

UrlHelper.Configure(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>(); // api/

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var logger = app.Services.GetRequiredService<ILogger<StoreContextSeedLoggerCategory>>();

    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, userManager, roleManager, logger);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
