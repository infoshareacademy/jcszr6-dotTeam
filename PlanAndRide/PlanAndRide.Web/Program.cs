using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using PlanAndRide.Database.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<PlanAndRide.BusinessLogic.Route>, RouteRepository>();
builder.Services.AddScoped<IRepository<Ride>, RideRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<Club>, ClubRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRideService, RideService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IClubService, ClubService>();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PlanAndRideContext>();

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<PlanAndRideContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
        options.UseLazyLoadingProxies();
        options.LogTo(Console.WriteLine, LogLevel.Information);
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();


// Configure the HTTP request pipeline.>
if (!app.Environment.IsDevelopment())
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRequestLocalization("en-US");

app.Run();
