using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Repository;
using StoreManagement.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Build services handing in here

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
/*builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
*/
//End
var app = builder.Build();
//Seed method
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Customer",
    pattern: "{area=Customer}/{controller=Order}/{id?}");

app.Run();
