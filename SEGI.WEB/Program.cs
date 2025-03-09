using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Helpers;
using SEGI.Data;
using SEGI.Services.Extenstions;
using SEGI.WEB.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //// Enable lazy loading proxies
    //options.UseLazyLoadingProxies();

    // Configure SQL Server with the connection string
    options.UseSqlServer(connectionString, options => options.CommandTimeout(180));
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // 50 MB
});
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; // Adjust as needed
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue; // Adjust as needed
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(3); // Adjust as needed
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.User.RequireUniqueEmail = true;
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.SignIn.RequireConfirmedEmail = true;
    // ????? ??? ????????? ?????? ?????? ?????? ??? ??? ??????
    config.Lockout.MaxFailedAccessAttempts = 3;
    // ??? ??? ?????? ??? ????? ??? ????????? ??????
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);


}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
.AddDefaultUI();
builder.Services.AddControllersWithViews(options =>
{
}).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Enable detailed routing logs

builder.Services.AddSingleton(new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new SEGI.Services.Mapper.AutoMapper());
}).CreateMapper());
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Only send cookie over HTTPS

});

builder.Services.AddHttpClient();

builder.Services.RegisterServices();
// Register your custom ApplicationUserManager

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443; // Specify the port for HTTPS (usually 443)
});

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2); // Token lifespan
});



builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}



app.UseHttpsRedirection(); // Redirect all HTTP requests to HTTPS
app.UseSession(); // Enable session middleware
app.UseHttpsRedirection();

//app.UseMiddleware<RedirectIfNotAuthenticatedMiddleware>();
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self';");
//    await next();
//});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Authenticate the user (required if you're using Identity or authentication)
app.UseAuthorization();  // Authorize the user (important to place it here)

//app.UseMiddleware<PurchaseCheckMiddleware>();

app.MapRazorPages();

app.UseRouting();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next.Invoke();
});
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
   name: "ControlPanel",
   pattern: "{area=ControlPanel}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}"
            );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapRazorPages();
});
app.MapRazorPages();

app.SeedDb().Run();
