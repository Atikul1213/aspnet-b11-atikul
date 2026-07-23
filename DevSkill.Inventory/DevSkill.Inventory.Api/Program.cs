
#region BootStrap Logger

using DevSkill.Core.Infrastructure.Extensions;
using DevSkill.Inventory.Application.Abstractions.Services;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
             .WriteTo.File("Logs/web-log-.txt",
             rollingInterval: RollingInterval.Day)
             .CreateBootstrapLogger();
#endregion

try
{
    Log.Information("Application is starting...");


    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly();

    #region Serilog configure
    builder.Host.UseSerilog((context, lc) => lc
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .ReadFrom.Configuration(builder.Configuration)
      );
    #endregion

    #region AutoMapper Configuration

    builder.Services.AddAutoMapper(
        cfg => { },
        AppDomain.CurrentDomain.GetAssemblies());

    #endregion


    #region Dependency Injection

    builder.Services.AddEmailMessagingServices
      <ApplicationUser, ApplicationRole, ApplicationUserClaim, ApplicationUserRole,
      ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>();
    builder.Services.AddDependencyInjection(connectionString, migrationAssembly.FullName!);

    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddCaptchaService();
    builder.Services.AddHttpContextAccessor();

    #endregion

    #region ApplicationDbContext
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddControllersWithViews();
    #endregion

    #region Identity Configuration
    builder.Services.AddIdentity();
    builder.Services.AddJwtAuthentication(
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"]
    );
    builder.Services.AddJwtAuthorization();
    #endregion
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    Log.Information("Application started successfully.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}