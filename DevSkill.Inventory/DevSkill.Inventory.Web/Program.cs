using Cortex.Mediator.DependencyInjection;
using DevSkill.Core.Infrastructure.Extensions;
using DevSkill.Inventory.Application.Abstractions.Services;
using DevSkill.Inventory.Application.Features.Products.Commands.CreateProduct;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Web.Extensions;
using DevSkill.Inventory.Web.Models;
using DevSkill.Inventory.Web.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

#region BootStrap Logger

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

    #region Autofac Configuration
    //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    //builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    //{
    //    containerBuilder.RegisterModule(new WebModule(connectionString, migrationAssembly?.FullName));
    //});
    #endregion

    #region Serilog configure
    builder.Host.UseSerilog((context, lc) => lc
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .ReadFrom.Configuration(builder.Configuration)
      );
    #endregion

    #region Cortex.Mediator Configuration

    builder.Services.AddCortexMediator(
        builder.Configuration,
        [typeof(Program), typeof(CreateProductCommandHandler)],
        options =>
        {
            options.AddDefaultBehaviors();
        }
     );

    #endregion

    #region AutoMapper Configuration
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Add Identity

    builder.Services.AddIdentity();

    //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    //    .AddEntityFrameworkStores<ApplicationDbContext>();

    #endregion

    #region Policy Based Authorization
    builder.Services.AddPolicy();
    #endregion

    #region Dependency Injection

    builder.Services.AddEmailMessagingServices
      <ApplicationUser, ApplicationRole, ApplicationUserClaim, ApplicationUserRole,
      ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>();
    builder.Services.AddDependencyInjection(connectionString, migrationAssembly.FullName!);
    builder.Services.AddFacebookAuthentication(builder.Configuration["Authentication:Facebook:AppId"]!, builder.Configuration["Authentication:Facebook:AppSecret"]!);
    builder.Services.AddGoogleAuthentication(builder.Configuration["Authentication:Google:ClientId"]!, builder.Configuration["Authentication:Google:ClientSecret"]!);

    builder.Services.AddScoped<IUserInfoService, UserInfoService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddCaptchaService();
    builder.Services.AddHttpContextAccessor();

    #endregion

    #region Docker_Configuration
    //builder.WebHost.UseUrls("http://*:80");
    #endregion
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddControllersWithViews();

    #region Razor pages
    builder.Services.AddRazorPages();
    #endregion

    #region Fluent Validation
    builder.Services.AddFluentValidationConfiguration();
    #endregion

    #region Email Configuration
    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
    #endregion

    #region AWS Bucket Configuration
    builder.Services.Configure<AwsOptions>(builder.Configuration.GetSection("AWS"));

    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    #region Area Configuration

    app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    #endregion

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var dataSeeder = services.GetRequiredService<IDataSeeder>();
            await dataSeeder.SeedAsync();
            Log.Information("Data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during data seeding.");
        }
    }

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
