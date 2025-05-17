using InventoryManagementNewVision.Data;
using InventoryManagementNewVision.Services.Dapper.IInterfaces;
using InventoryManagementNewVision.Services.InventoryServices;
using InventoryManagementNewVision.Services.InventoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
}).AddRazorRuntimeCompilation();


#region DEPENDENCY FOR REPO AND SERVICES
builder.Services.AddScoped<IInventoryServices, InventoryServices>();
#endregion


#region Database Settings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));


builder.Services.AddScoped<IDapper, InventoryManagementNewVision.Services.Dapper.Dapper>();

#endregion

#region Auth Related Settings

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);

    options.LoginPath = "/Home/Home/Index";
    options.AccessDeniedPath = "/Home/Home/AccessDenied";
    options.SlidingExpiration = true;
});
#endregion


#region Areas Config
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});
#endregion

builder.Services.AddDistributedMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
     builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
        );
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.IsEssential = true;
});
#endregion Configure Services

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();
app.UseCors("_myAllowSpecificOrigins");

app.UseEndpoints(endpoints =>
{
    // Area-specific route
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default route pointing to the Home area
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
