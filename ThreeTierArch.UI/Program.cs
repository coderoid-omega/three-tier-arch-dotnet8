using Microsoft.EntityFrameworkCore;
using ThreeTierArch.Repositories;
using ThreeTierArch.Repositories.Implementations;
using ThreeTierArch.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Adding MVC
builder.Services.AddControllersWithViews();
//Adding DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),b => b.MigrationsAssembly("ThreeTierArch.UI")));

//Adding DI for Application Classes
builder.Services.AddScoped<ICountryRepo, CountryRepo>();
builder.Services.AddScoped<IStateRepo, StateRepo>();
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IUserInfoRepo, UserInfoRepo>();
builder.Services.AddScoped<IUtilityRepo, UtilityRepo>();
builder.Services.AddScoped<ISkillRepo, SkillRepo>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Session Configurations
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(1);
    option.Cookie.HttpOnly = true;
});

var app = builder.Build();

//MIDDLEWARES
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Session to be added before routing middleware
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
