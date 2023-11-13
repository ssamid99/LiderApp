using FluentValidation;
using FluentValidation.AspNetCore;
using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.AppCode.Providers;
using LiderApp.Domain.Models.DbContexts;
using LiderApp.Domain.Models.Entity.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
    //var policy = new AuthorizationPolicyBuilder()
    //.RequireAuthenticatedUser()
    //.Build();

    //cfg.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddDbContext<LiderAppDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"));
})
    .AddIdentity<LiderUser, LiderRole>()
    .AddEntityFrameworkStores<LiderAppDbContext>()
    .AddDefaultTokenProviders();

#region Account Configurations
builder.Services.Configure<IdentityOptions>(cfg =>
{

    cfg.Password.RequireDigit = true;
    cfg.Password.RequireUppercase = true;
    cfg.Password.RequireLowercase = true;
    cfg.Password.RequiredUniqueChars = 1;
    cfg.Password.RequireNonAlphanumeric = true;
    cfg.Password.RequiredLength = 8;

    cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@/";
    cfg.User.RequireUniqueEmail = true;

    cfg.Lockout.MaxFailedAccessAttempts = 3;
    cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
});

//builder.Services.ConfigureApplicationCookie(cfg =>
//{

//    cfg.LoginPath = "/signin.html";
//    cfg.AccessDeniedPath = "/accessdenied.html";
//    cfg.ExpireTimeSpan = new TimeSpan(0, 15, 0);
//    cfg.Cookie.Name = "Lider";
//});

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();
#endregion

builder.Services.AddFluentValidationAutoValidation();
var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("LiderApp.")).ToArray();
builder.Services.AddMediatR(assemblies);
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddValidatorsFromAssemblies(assemblies);

var customServicesConfiguration = new InterfaceServices();
customServicesConfiguration.ConfigureServices(builder.Services);






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

//app.UseAuthentication();
//app.UseAuthorization();

#region MapControllerRouting Configurations
app.UseEndpoints(cfg =>
{
    cfg.MapControllerRoute(
        name: "default-signin",
        pattern: "signin.html",
        defaults: new
        {
            area = "",
            controller = "account",
            action = "signin"
        });
    cfg.MapControllerRoute(
        name: "default-accessdenied",
        pattern: "accessdenied.html",
        defaults: new
        {
            area = "",
            controller = "account",
            action = "accessdenied"
        });
    cfg.MapControllerRoute(
        name: "default-register",
        pattern: "register.html",
        defaults: new
        {
            area = "",
            controller = "account",
            action = "register"
        });

    cfg.MapAreaControllerRoute(
        name: "admin",
        areaName: "admin",
        pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}");
    cfg.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#endregion

app.Run();