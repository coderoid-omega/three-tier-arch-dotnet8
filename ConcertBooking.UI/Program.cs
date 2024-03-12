using ConcertBooking.Repositories;
using ConcertBooking.Repositories.Implementations;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ConcertBooking.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ConcertBooking.UI"));
});

//Default Identity provider does not have role settings in it, so for Activating Role management in Identity, default is commented here
/*builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();*/

//AddIdentity and AddIdentityCore are the two custom Identity Provider, Here we are using AddIdentity method

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders() //This is used to set default token generation scheme when user logs in to the system
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IArtistRepo, ArtistRepo>();
builder.Services.AddScoped<IVenueRepo, VenueRepo>();
builder.Services.AddScoped<IConcertRepo, ConcertRepo>();
builder.Services.AddScoped<IUtilityRepo, UtilityRepo>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITicketRepo, TicketRepo>();
builder.Services.AddScoped<IDbInitialize, DbInitialize>();
//EmailSender Class to be implemented when using Custom identity provider instead of Default Identity Provider
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Seeding Data here
SeedingData();
void SeedingData()
{
    //To use any service or manually call any service without DI, we use Scope Method
    using(var scope = app.Services.CreateScope())
    {
        var dbSeed = scope.ServiceProvider.GetRequiredService<IDbInitialize>();
        dbSeed.Seed().GetAwaiter().GetResult();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
