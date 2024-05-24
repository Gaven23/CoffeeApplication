using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoffeeApplication.Data;
using CoffeeApplication.Areas.Identity.Data;
using CoffeeApplication.HttpClients;
using CoffeeApplication;
using CoffeeApplication.Data.DataStore;
using CoffeeApplication.DataServices;

IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                              .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                                                              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                                                              .Build();
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CoffeeApplicationContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'CoffeeApplicationContextConnection' not found.");

builder.Services.Configure<AppSettings>(configuration);
var appSettings = configuration.Get<AppSettings>();

builder.Services.AddDbContext<CoffeeApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CoffeeApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CoffeeApplicationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<CoffeeApplication.Interfaces.IDataStore, OrderDataService>();
builder.Services.AddScoped<OrderDataService>();
builder.Services.AddHttpClient<CoffeeApiHttpClient>(client =>
{
    if (appSettings?.CoffeeSettings.CoffeeApiUrl == null)
    {
        throw new ArgumentNullException(nameof(appSettings.CoffeeSettings.CoffeeApiUrl), innerException: null);
    }
    client.BaseAddress = new Uri(appSettings.CoffeeSettings.CoffeeApiUrl);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication middleware is used
app.UseAuthorization();

// Middleware to redirect unauthenticated users to the login page
app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated && 
        !context.Request.Path.Value.Contains("/Identity/Account/Login") && 
        !context.Request.Path.Value.Contains("/Identity/Account/Register"))
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

