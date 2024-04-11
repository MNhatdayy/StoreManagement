using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;

using StoreManagement.Repository;
using StoreManagement.Service;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Build services handing in here

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddRazorPages();

builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddTransient<IMenuDetailRepository, MenuDetailRepository>();
builder.Services.AddScoped<IMenuDetailService, MenuDetailService>();
builder.Services.AddTransient<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddTransient<IFoodCategoryRepository, FoodCategoryRepository>();
builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddTransient<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Auth/login";
    options.LogoutPath = $"/Auth/logout";
    options.LogoutPath = $"/Auth/login";


});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.Cookie.Name = "User";
        option.LoginPath = "/auth/login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    }
);

var app = builder.Build();
//End
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

app.MapRazorPages();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}/{id?}");
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area=Admin}/{controller}/{action}/{id?}");
    endpoints.MapControllerRoute(
        name: "Customer",
        pattern: "{area=Customer}/{controller=Order}/{id?}");
    endpoints.MapControllerRoute(
        name: "Owner",
        pattern: "{area=Owner}/{controller=Home}/{action=Index}/{id?}");

});



app.Run();
